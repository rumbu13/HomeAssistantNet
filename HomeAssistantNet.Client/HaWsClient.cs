using HomeAssistantNet.Client.Internal;
using JorgeSerrano.Json;
using System.IO.Pipelines;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;

namespace HomeAssistantNet.Client;

public sealed class HaWsClient : IHaWsClient
{
    HaWsClientOptions? options;
    CancellationTokenSource? stopCancellation;

    bool isRunning;
    bool isDisposed;
    int eventSubcriptionId = -1;

    int messageId;

    Task? handleTask;
    Task? runTask;

    IHaWsConnection? connection;

    Uri? wsUri;

    public bool IsConnected
        => connection != null && connection.IsConnected;

    readonly Channel<HaWsMessage> queue = Channel.CreateUnbounded<HaWsMessage>();

    readonly HaWsMessageEventArgs messageEventArgs = new();
    readonly HaWsEventEventArgs eventEventArgs = new();

    public event EventHandler<HaWsConnectedEventArgs>? Connected;
    public event EventHandler<HaWsDisconnectedEventArgs>? Disconnected;
    public event EventHandler<HaWsMessageEventArgs>? MessageReceived;

    private event EventHandler<HaWsEventEventArgs>? eventReceived;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1065:Do not raise exceptions in unexpected locations", Justification = "Don't say")]
    public event EventHandler<HaWsEventEventArgs>? EventReceived
    {
        add
        {
            eventReceived += value;
            _ = HandleEventsSubscription(CancellationToken.None)
                .ContinueWith(t => throw t.Exception!, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Current);
        }
        remove
        {
            eventReceived -= value;
            _ = HandleEventsSubscription(CancellationToken.None)
               .ContinueWith(t => throw t.Exception!, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Current);

        }
    }

    private EventHandler<HaWsInternalMessageEventArgs>? internalMessageReceived;

    void CheckNotDisposed()
    {
        if (isDisposed)
            throw new ObjectDisposedException(nameof(HaWsClient));
    }

    void CheckRunning(bool running)
    {
        if (isRunning != running)
            throw new InvalidOperationException("Home Assistant Client is already " + (running ? "stopped" : "started"));
    }

    void CheckConnected(bool connected)
    {
        if (IsConnected != connected)
            throw new InvalidOperationException("Cannot perform this operation while " + (connected ? "disconnected" : "connected"));
    }

    public bool IsRunning => isRunning;


    [System.Diagnostics.CodeAnalysis.SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification = "False positive")]
    async Task ConnectAsync()
    {
        CheckNotDisposed();
        CheckConnected(false);
        eventSubcriptionId = -1;
        connection?.Dispose();
        connection = new HaWsConnection();
        using var timeoutCancellation = new CancellationTokenSource(options!.ConnectTimeout);
        using var combinedCancellation = CancellationTokenSource.CreateLinkedTokenSource(stopCancellation!.Token, timeoutCancellation.Token);
        await connection.StartAsync(wsUri!, combinedCancellation.Token).ConfigureAwait(false);
        var welcome = await ReceiveRawAsync<HaWsMessage>().ConfigureAwait(false);
        if (welcome.Type != "auth_required")
            throw new InvalidDataException($"Unexpected message type: '{welcome.Type}'");
        await SendRawAsync(new
        {
            type = "auth",
            access_token = options!.Token
        }).ConfigureAwait(false);
        var authResponse = await ReceiveRawAsync<HaWsMessage>().ConfigureAwait(false);
        if (authResponse.Type == "auth_invalid")
            throw new UnauthorizedAccessException(authResponse.Message);
        if (authResponse.Type != "auth_ok")
            throw new InvalidDataException($"Unexpected message type: '{authResponse.Type}'");
        eventSubcriptionId = -1;
        if (eventReceived is not null)
        {
            var cmd = new HaSubcribeEvents();
            cmd.Id = Interlocked.Increment(ref messageId);
            await SendRawAsync(cmd).ConfigureAwait(false);
            var response = await ReceiveRawAsync<HaWsMessage>().ConfigureAwait(false);
            if (response.Success is null || !response.Success.Value)
                throw new InvalidDataException(response.Message);
            eventSubcriptionId = cmd.Id;
        }
        Connected?.Invoke(this, new HaWsConnectedEventArgs(authResponse.HaVersion));        
    }

    async Task HandleEventsSubscription(CancellationToken cancellationToken)
    {
        if (!IsConnected)
            return;

        if (handleTask is null || handleTask.IsCompleted)
            return;

        if (eventReceived is not null && eventSubcriptionId < 0)
        {
            var cmd = new HaSubcribeEvents();
            await SendAsync<HaSubcribeEvents, object>(cmd, cancellationToken).ConfigureAwait(false);
            eventSubcriptionId = cmd.Id;

        }
        else if (eventReceived is null && eventSubcriptionId >= 0)
        {
            await SendAsync<HaUnsubcribeEvents, object>(new HaUnsubcribeEvents(eventSubcriptionId),
                cancellationToken).ConfigureAwait(false);
            eventSubcriptionId = -1;
        }

    }


    async Task HandleMessagesAsync()
    {
        await foreach (var message in queue.Reader.ReadAllAsync(stopCancellation!.Token))
        {
            bool dispatch = true;
            if (internalMessageReceived is not null)
            {
                var args = new HaWsInternalMessageEventArgs(message);
                internalMessageReceived.Invoke(this, args);
                dispatch = args.Dispatch;
            }

            if (dispatch && MessageReceived is not null)
            {
                messageEventArgs.Message = message;
                MessageReceived.Invoke(this, messageEventArgs);
            }

            if (dispatch && eventReceived is not null && message.Event is not null)
            {
                eventEventArgs.Event = message.Event;
                eventReceived.Invoke(this, eventEventArgs);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification = "False positive")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "That's the point, retrying")]
    async Task RunAsync()
    {
        bool reconnect = true;
        HaWsDisconnectReason disconnectReason = HaWsDisconnectReason.ServerInitiated;
        Exception? error = null;
        bool retry = false;
        var reconnectDelay = options!.ReconnectMinTimeout;
        while (reconnect && !stopCancellation!.IsCancellationRequested)
        {
            try
            {
                disconnectReason = HaWsDisconnectReason.ServerInitiated;
                error = null;
                if (retry)
                    await Task.Delay(reconnectDelay, stopCancellation.Token).ConfigureAwait(false);
                await ConnectAsync().ConfigureAwait(false);
                reconnectDelay = options!.ReconnectMinTimeout;
                handleTask = Task.Factory.StartNew(async () =>
                    await HandleMessagesAsync().ConfigureAwait(false), stopCancellation.Token,
                    TaskCreationOptions.LongRunning, TaskScheduler.Default);
                while (!stopCancellation!.IsCancellationRequested && IsConnected)
                {
                    var message = await connection!.ReceiveAsync<HaWsMessage>(stopCancellation.Token).ConfigureAwait(false);
                    await queue.Writer.WriteAsync(message, stopCancellation.Token).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException ex) when (ex.CancellationToken == stopCancellation.Token)
            {
                disconnectReason = HaWsDisconnectReason.UserInitiated;
                reconnect = false;
            }
            catch (OperationCanceledException)
            {
                disconnectReason = HaWsDisconnectReason.Timeout;
            }
            catch (UnauthorizedAccessException ex)
            {
                error = ex;
                disconnectReason = HaWsDisconnectReason.AuthenticationFailed;
                reconnect = false;
            }
            catch (InvalidDataException ex)
            {
                error = ex;
                disconnectReason = HaWsDisconnectReason.InvalidData;
            }
            catch (WebSocketException ex)
            {
                error = ex;
                disconnectReason = HaWsDisconnectReason.CommunicationError;
            }
            catch (Exception ex)
            {
                error = ex;
                disconnectReason = HaWsDisconnectReason.Error;
                reconnect = false;
            }

            if (error is not null)
            {
                reconnectDelay = reconnectDelay.Add(TimeSpan.FromSeconds(1));
                if (reconnectDelay > options!.ReconnectMaxTimeout)
                    reconnectDelay = options!.ReconnectMaxTimeout;
            }
            queue.Writer.Complete();
            if (IsConnected)
                await connection!.StopAsync(stopCancellation.Token).ConfigureAwait(false);
            connection!.Dispose();

            if (Disconnected is not null)
            {
                var disconnectEventArgs = new HaWsDisconnectedEventArgs(reconnect, disconnectReason, error);
                Disconnected.Invoke(this, disconnectEventArgs);
                reconnect = disconnectEventArgs.Reconnect;
            }
        }

        isRunning = false;
        if (!stopCancellation!.IsCancellationRequested)
            stopCancellation.Cancel();
    }


    public void Start(HaWsClientOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        CheckNotDisposed();
        CheckRunning(false);

        isRunning = true;
        this.options = options;
        stopCancellation = new CancellationTokenSource();
        var schema = options!.UseWss ? "wss" : "ws";
        wsUri = new Uri($"{schema}://{options.Host}:{options.Port}/api/websocket");
        runTask = Task.Factory.StartNew(async ()
            => await RunAsync().ConfigureAwait(false), stopCancellation.Token,
            TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    public void Stop()
    {
        CheckNotDisposed();
        CheckRunning(true);

        if (!stopCancellation!.IsCancellationRequested)
            stopCancellation.Cancel();
        connection?.Dispose();
        stopCancellation?.Dispose();
        stopCancellation = null;
        isRunning = false;
    }


    Task SendRawAsync<T>(T value)
    {
        using var timeoutCancellation = new CancellationTokenSource(options!.SendTimeout);
        using var combinedCancellation = CancellationTokenSource.CreateLinkedTokenSource(stopCancellation!.Token, timeoutCancellation.Token);
        return connection!.SendAsync(value, combinedCancellation.Token);
    }

    Task<TResult> ReceiveRawAsync<TResult>()
    {
        using var timeoutCancellation = new CancellationTokenSource(options!.ReceiveTimeout);
        using var combinedCancellation = CancellationTokenSource.CreateLinkedTokenSource(stopCancellation!.Token, timeoutCancellation.Token);
        return connection!.ReceiveAsync<TResult>(combinedCancellation.Token);
    }

    public async Task<TResult?> SendAsync<T, TResult>(T value, CancellationToken cancellationToken) where T : HaWsCommand
    {
        CheckNotDisposed();
        CheckConnected(true);
        ArgumentNullException.ThrowIfNull(value);

        var id = Interlocked.Increment(ref messageId);

        using var combinedCancellation = CancellationTokenSource.CreateLinkedTokenSource(stopCancellation!.Token, cancellationToken);
        var tcs = new TaskCompletionSource<HaWsMessage?>();
        var handler = new EventHandler<HaWsInternalMessageEventArgs>((sender, e) =>
        {
            tcs.TrySetResult(e.Message);
            e.Dispatch = false;
        });
        internalMessageReceived += handler;
        try
        {
            value.Id = messageId;
            await SendRawAsync(value).ConfigureAwait(false);
            var message = await tcs.Task.WaitAsync(combinedCancellation.Token).ConfigureAwait(false);
            if (message is not null && (message.Success.HasValue && message.Success.Value || message.Type == "pong"))
            {
                if (message.Result.HasValue)
                    return message.Result.Value.Deserialize<TResult>(IHaWsConnection.JsonOptions);
                else
                    return default;
            }
            else
                throw new InvalidDataException(message is null ? "Empty response received" : message.Error?.Message);
        }
        finally
        {
            internalMessageReceived -= handler;
        }
    }

    public void Dispose()
    {
        if (!isDisposed)
        {
            isDisposed = true;
            Connected = null;
            Disconnected = null;
            MessageReceived = null;
            internalMessageReceived = null;
            try
            {

                if (!stopCancellation!.IsCancellationRequested)
                    stopCancellation.Cancel();
                connection?.Dispose();
            }
            finally
            {
                stopCancellation?.Dispose();
                stopCancellation = null;
                isRunning = false;
            }
        }
    }
}
