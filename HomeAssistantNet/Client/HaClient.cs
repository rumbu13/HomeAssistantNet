using HomeAssistantNet.Client.Internal;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text.Json;

namespace HomeAssistantNet.Client;

public sealed class HaClient : IHaClient
{
    HaClientOptions? options;
    CancellationTokenSource? stopCancellation;

    bool isRunning;
    bool isDisposed;
    int eventSubcriptionId = -1;

    int messageId;

    string? haVersion;

    Task? runTask;

    IHaConnection? connection;

    Uri? wsUri;

    public bool IsConnected
        => connection != null && connection.IsConnected;

    readonly HaMessageEventArgs messageEventArgs = new();

    TaskCompletionSource connectionWaiter = new(TaskCreationOptions.RunContinuationsAsynchronously);
    TaskCompletionSource disconnectionWaiter = new(TaskCreationOptions.RunContinuationsAsynchronously);

    public event EventHandler<HaConnectedEventArgs>? Connected;
    public event EventHandler<HaDisconnectedEventArgs>? Disconnected;
    private event EventHandler<HaEventEventArgs>? eventReceived;


    public event EventHandler<HaEventEventArgs>? EventReceived
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


    private event Func<HaMessage, bool>? internalProcessMessage;
    public event EventHandler<HaConnectingEventArgs>? Connecting;

    void CheckNotDisposed()
    {
        if (isDisposed)
            throw new ObjectDisposedException(nameof(HaClient));
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


    async Task ConnectAsync()
    {
        CheckNotDisposed();
        CheckConnected(false);
        eventSubcriptionId = -1;
        connection?.Dispose();
        connection = new HaConnection();
        using var timeoutCancellation = new CancellationTokenSource(options!.ConnectTimeout);
        using var combinedCancellation = CancellationTokenSource.CreateLinkedTokenSource(stopCancellation!.Token, timeoutCancellation.Token);
        await connection.StartAsync(wsUri!, combinedCancellation.Token).ConfigureAwait(false);
        var welcome = await ReceiveRawAsync<HaMessage>().ConfigureAwait(false);
        if (welcome?.Type != "auth_required")
            throw new InvalidDataException($"Unexpected message type: '{welcome?.Type}'");
        await SendRawAsync(new
        {
            type = "auth",
            access_token = options!.Token
        }).ConfigureAwait(false);

        var authResponse = await ReceiveRawAsync<HaMessage>().ConfigureAwait(false);
        if (authResponse?.Type == "auth_invalid")
            throw new UnauthorizedAccessException(authResponse.Message);
        if (authResponse?.Type != "auth_ok")
            throw new InvalidDataException($"Unexpected message type: '{authResponse?.Type}'");

        haVersion = authResponse!.HaVersion;
        eventSubcriptionId = -1;
        if (eventReceived is not null)
        {
            var cmd = new HaSubcribeEvents
            {
                Id = Interlocked.Increment(ref messageId)
            };
            await SendRawAsync(cmd).ConfigureAwait(false);
            var response = await ReceiveRawAsync<HaMessage>().ConfigureAwait(false);
            if (response?.Success is null || !response.Success.Value)
                throw new InvalidDataException(response?.Message);
            eventSubcriptionId = cmd.Id;
        }

    }

    async Task HandleEventsSubscription(CancellationToken cancellationToken)
    {
        if (!IsConnected)
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


    async Task RunAsync()
    {

        bool reconnect = true;
        HaDisconnectReason disconnectReason = HaDisconnectReason.ConnectionClosed;
        Exception? error = null;
        bool retry = false;
        var reconnectDelay = options!.ReconnectMinTimeout;
        int attempt = 1;
        while (reconnect && !stopCancellation!.IsCancellationRequested)
        {
            
            try
            {
                if (Connecting is not null)
                    _ = Task.Run(() => Connecting?.Invoke(this, new HaConnectingEventArgs(attempt)));
                disconnectReason = HaDisconnectReason.ConnectionClosed;
                error = null;
                if (retry)
                    await Task.Delay(reconnectDelay, stopCancellation.Token).ConfigureAwait(false);
                retry = true;
                await ConnectAsync().ConfigureAwait(false);
                reconnectDelay = options!.ReconnectMinTimeout;
                attempt = 1;
                if (Connected is not null)
                    _ = Task.Run(() => Connected?.Invoke(this, new HaConnectedEventArgs(haVersion)));
                connectionWaiter.TrySetResult();
                disconnectionWaiter = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
                while (!stopCancellation!.IsCancellationRequested && IsConnected)
                {
                    var message = await connection!.ReceiveAsync<HaMessage>(stopCancellation.Token).ConfigureAwait(false);
                    if (message == null)
                        break;
                    bool dispatch = true;
                    if (internalProcessMessage is not null)
                        dispatch = internalProcessMessage(message);
                    if (dispatch && eventReceived is not null && message.Event is not null)
                    {
                        var eventEventArgs = new HaEventEventArgs(message.Event);
                        foreach (EventHandler<HaEventEventArgs> handler in eventReceived.GetInvocationList())
                            _ = Task.Run(() => handler.Invoke(this, eventEventArgs));
                    }
                }
            }
            catch (OperationCanceledException ex) when (ex.CancellationToken == stopCancellation.Token)
            {
                disconnectReason = HaDisconnectReason.UserInitiated;
                reconnect = false;
            }
            catch (OperationCanceledException)
            {
                disconnectReason = HaDisconnectReason.Timeout;
            }
            catch (UnauthorizedAccessException ex)
            {
                error = ex;
                disconnectReason = HaDisconnectReason.AuthenticationFailed;
                reconnect = false;
            }
            catch (InvalidDataException ex)
            {
                error = ex;
                disconnectReason = HaDisconnectReason.InvalidData;
            }
            catch (WebSocketException ex)
            {
                error = ex;
                disconnectReason = HaDisconnectReason.CommunicationError;
            }
            catch (Exception ex)
            {
                error = ex;
                disconnectReason = HaDisconnectReason.Error;
                reconnect = false;
            }

            if (error is not null)
            {
                reconnectDelay = reconnectDelay.Add(TimeSpan.FromSeconds(1));
                if (reconnectDelay > options!.ReconnectMaxTimeout)
                    reconnectDelay = options!.ReconnectMaxTimeout;
                attempt++;
            }

            if (IsConnected)
                await connection!.StopAsync(stopCancellation.Token).ConfigureAwait(false);
            connection!.Dispose();

            if (Disconnected is not null)
                _ = Task.Run(() => Disconnected.Invoke(this, new HaDisconnectedEventArgs(reconnect, disconnectReason, error)));            
            disconnectionWaiter.TrySetResult();            
            connectionWaiter = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        }

        isRunning = false;
        if (!stopCancellation!.IsCancellationRequested)
            stopCancellation.Cancel();


    }


    public void Start(HaClientOptions options)
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

        Trace.WriteLine("");
    }

    public void Stop()
    {
        CheckNotDisposed();
        CheckRunning(true);

        if (!stopCancellation!.IsCancellationRequested)
            stopCancellation.Cancel();
        connection?.Dispose();
        stopCancellation?.Dispose();
        isRunning = false;
    }


    Task SendRawAsync<T>(T value)
    {
        using var timeoutCancellation = new CancellationTokenSource(options!.SendTimeout);
        using var combinedCancellation = CancellationTokenSource.CreateLinkedTokenSource(stopCancellation!.Token, timeoutCancellation.Token);
        return connection!.SendAsync(value, combinedCancellation.Token);
    }

    Task<TResult?> ReceiveRawAsync<TResult>()
    {
        using var timeoutCancellation = new CancellationTokenSource(options!.ReceiveTimeout);
        using var combinedCancellation = CancellationTokenSource.CreateLinkedTokenSource(stopCancellation!.Token, timeoutCancellation.Token);
        return connection!.ReceiveAsync<TResult>(combinedCancellation.Token);
    }

    public async Task<TResult?> SendAsync<T, TResult>(T value, CancellationToken cancellationToken) where T : HaCommand
    {
        CheckNotDisposed();
        CheckConnected(true);
        ArgumentNullException.ThrowIfNull(value);

        var id = Interlocked.Increment(ref messageId);

        using var combinedCancellation = CancellationTokenSource.CreateLinkedTokenSource(stopCancellation!.Token, cancellationToken);


        var tcs = new TaskCompletionSource<HaMessage?>();

        var h = new Func<HaMessage, bool>(m =>
        {
            if (m.Id == id)
            {
                tcs.TrySetResult(m);
                return false;
            }
            return true;
        });

        internalProcessMessage += h;
        try
        {
            value.Id = messageId;
            await SendRawAsync(value).ConfigureAwait(false);
            var message = await tcs.Task.WaitAsync(combinedCancellation.Token).ConfigureAwait(false);
            if (message is not null && (message.Success.HasValue && message.Success.Value || message.Type == "pong"))
            {
                if (message.Result.HasValue)
                    return message.Result.Value.Deserialize<TResult>(HaOptions.DefaultJsonSerializerOptions);
                else
                    return default;
            }
            else
                throw new InvalidDataException(message is null ? "Empty response received" : message.Error?.Message);
        }
        finally
        {
            internalProcessMessage -= h;
        }
    }

    public void Dispose()
    {
        if (!isDisposed)
        {
            isDisposed = true;
            Connected = null;
            Connecting = null;
            Disconnected = null;
            internalProcessMessage = null;
            try
            {

                if (!stopCancellation!.IsCancellationRequested)
                    stopCancellation.Cancel();
                connection?.Dispose();
            }
            finally
            {
                stopCancellation?.Dispose();
                isRunning = false;
            }
        }
    }

    public Task WaitForConnectionAsync(bool stopOnError, CancellationToken cancellationToken = default)
    {
        if (stopOnError)
            return Task.WhenAny(connectionWaiter.Task, disconnectionWaiter.Task);
        return connectionWaiter.Task;
    }
}
