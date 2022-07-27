using HomeAssistantNet.Helpers;
using System.IO.Pipelines;
using System.Net.WebSockets;
using System.Text.Json;

namespace HomeAssistantNet.Client.Internal;


internal sealed class HaWsConnection : IHaWsConnection
{
    public bool IsConnected
        => !isDisposed && socket != null && socket.State == WebSocketState.Open && !socket.CloseStatus.HasValue;


    ClientWebSocket? socket;
    Pipe? pipe;
    SemaphoreSlim? semaphore;


    bool isDisposed;

    void CheckConnected()
    {
        if (!IsConnected)
            throw new InvalidOperationException("Cannot perform this operation while disconnected");
    }

    void CheckNotDisposed()
    {
        if (isDisposed)
            throw new ObjectDisposedException(nameof(HaWsConnection));
    }

    public Task StartAsync(Uri uri, CancellationToken cancellationToken)
    {
        CheckNotDisposed();
        if (IsConnected)
            return Task.CompletedTask;

        socket = new ClientWebSocket();
        pipe = new Pipe();
        semaphore = new SemaphoreSlim(1);
        return socket.ConnectAsync(uri, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            if (socket?.State == WebSocketState.Closed)
                return socket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, cancellationToken);
            else if (socket?.State == WebSocketState.CloseReceived)
                return socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, cancellationToken);
            else if (socket?.State == WebSocketState.Connecting)
                socket.Abort();
        }
        finally
        {
            socket?.Dispose();
            semaphore?.Dispose();
            socket = null;
            semaphore = null;
            pipe = null;
        }
        return Task.CompletedTask;
    }


    public void Dispose()
    {
        if (!isDisposed)
        {
            isDisposed = true;
            semaphore?.Dispose();
            socket?.Dispose();
        }
    }

    async Task PipeWriteAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested && IsConnected)
            {
                var memory = pipe!.Writer.GetMemory();
                var result = await socket!.ReceiveAsync(memory, cancellationToken).ConfigureAwait(false);
                if (socket.State == WebSocketState.Open && result.MessageType != WebSocketMessageType.Close)
                {
                    pipe.Writer.Advance(result.Count);
                    await pipe.Writer.FlushAsync(cancellationToken).ConfigureAwait(false);
                    if (result.EndOfMessage)
                        break;
                }
                else if (socket.State == WebSocketState.CloseReceived)
                    await socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, cancellationToken)
                        .ConfigureAwait(false);
            }
        }
        finally
        {
            await pipe!.Writer.CompleteAsync().ConfigureAwait(false);
        }
    }

    async Task<T?> PipeReadAsync<T>(CancellationToken cancellationToken)
    {
        try
        {
            var message = await JsonSerializer.DeserializeAsync<T>(pipe!.Reader.AsStream(), HaOptions.DefaultJsonSerializerOptions,
                    cancellationToken).ConfigureAwait(false);
            return message;
        }
        catch (JsonException e) when (e.BytePositionInLine == 0)
        {
            return default;
        }
        finally
        {
            await pipe!.Reader.CompleteAsync().ConfigureAwait(false);
        }
    }

    public async Task<T?> ReceiveAsync<T>(CancellationToken cancellationToken)
    {
        CheckNotDisposed();
        CheckConnected();
        try
        {
            var readTask = PipeReadAsync<T>(cancellationToken);
            await PipeWriteAsync(cancellationToken).ConfigureAwait(false);
            return await readTask.ConfigureAwait(false);
        }
        finally
        {
            pipe!.Reset();
        }
    }

    public async Task SendAsync<T>(T message, CancellationToken cancellationToken)
    {
        CheckNotDisposed();
        CheckConnected();
        await semaphore!.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            var bytes = JsonSerializer.SerializeToUtf8Bytes(message, HaOptions.DefaultJsonSerializerOptions);
            await socket!.SendAsync(bytes, WebSocketMessageType.Text, true, cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            _ = semaphore!.Release();
        }
    }



}
