using HomeAssistantNet.Tools;
using System.IO.Pipelines;
using System.Net.WebSockets;
using System.Text.Json;

namespace HomeAssistantNet.Client.Internal;


internal sealed class HaConnection : IHaConnection
{
    private ClientWebSocket? _webSocket;
    private Pipe? _pipe;
    private SemaphoreSlim? _writeSemaphore;
    private SemaphoreSlim? _readSemaphore;
    private bool _disposed;
        
    private void ThrowIfNotConnected()
    {
        if (!IsConnected)
            throw new InvalidOperationException("Cannot perform this operation while disconnected");
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(HaConnection));
    }

    private async Task PipeWriteAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested && IsConnected)
            {
                var memory = _pipe!.Writer.GetMemory();
                var result = await _webSocket!.ReceiveAsync(memory, cancellationToken).ConfigureAwait(false);
                if (_webSocket.State == WebSocketState.Open && result.MessageType != WebSocketMessageType.Close)
                {
                    _pipe.Writer.Advance(result.Count);
                    await _pipe.Writer.FlushAsync(cancellationToken).ConfigureAwait(false);
                    if (result.EndOfMessage)
                        break;
                }
                else if (_webSocket.State == WebSocketState.CloseReceived)
                    await _webSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, cancellationToken)
                        .ConfigureAwait(false);
            }
        }
        finally
        {
            await _pipe!.Writer.CompleteAsync().ConfigureAwait(false);
        }
    }

    private async Task<T?> PipeReadAsync<T>(CancellationToken cancellationToken)
    {
        try
        {
            var stream = _pipe!.Reader.AsStream();
            var message = await JsonSerializer.DeserializeAsync<T>(stream, 
                HaTools.DefaultJsonSerializerOptions, cancellationToken).ConfigureAwait(false);
            return message;
        }
        catch (JsonException e) when (e.BytePositionInLine == 0)
        {
            return default;
        }
        finally
        {
            await _pipe!.Reader.CompleteAsync().ConfigureAwait(false);
        }
    }

    public Task ConnectAsync(Uri uri, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();

        if (IsConnected)
            return Task.CompletedTask;

        _webSocket = new ClientWebSocket();
        _pipe = new Pipe();
        _writeSemaphore = new SemaphoreSlim(1);
        _readSemaphore = new SemaphoreSlim(1);

        return _webSocket.ConnectAsync(uri, cancellationToken);
    }

    public Task CloseAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (_webSocket?.State == WebSocketState.Closed)
                return _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, cancellationToken);
            else if (_webSocket?.State == WebSocketState.CloseReceived)
                return _webSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, cancellationToken);
            else if (_webSocket?.State == WebSocketState.Connecting)
                _webSocket.Abort();
            return Task.CompletedTask;
        }
        finally
        {
            Dispose();
        }        
    }

    public bool IsConnected
        => !_disposed && _webSocket != null && _webSocket.State == WebSocketState.Open && !_webSocket.CloseStatus.HasValue;

    public void Dispose()
    {
        if (!_disposed)
        {
            _disposed = true;
            _readSemaphore?.Dispose();
            _writeSemaphore?.Dispose();
            _webSocket?.Dispose();
        }
    }

    public async Task<T?> ReceiveAsync<T>(CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        ThrowIfNotConnected();

        await _readSemaphore!.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var readTask = PipeReadAsync<T>(cancellationToken);
            await PipeWriteAsync(cancellationToken).ConfigureAwait(false);
            return await readTask.ConfigureAwait(false);
        }
        finally
        {
            _readSemaphore!.Release();
            SilentlyResetPipe();
        }
    }

    private void SilentlyResetPipe()
    {
        try
        {
            _pipe!.Reset();
        }
        catch (InvalidOperationException)
        {
            _pipe = new Pipe();
        }
    }

    public async Task SendAsync<T>(T message, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        ThrowIfNotConnected();

        await _writeSemaphore!.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var bytes = JsonSerializer.SerializeToUtf8Bytes(message, HaTools.DefaultJsonSerializerOptions);
            await _webSocket!.SendAsync(bytes, WebSocketMessageType.Text, true, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            _writeSemaphore!.Release();
        }
    }



}
