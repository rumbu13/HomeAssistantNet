using HomeAssistantNet.Api;
using HomeAssistantNet.Tools;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HomeAssistantNet.Client.Internal;

internal sealed class HaClient : IHaClient
{
    private HaClientOptions? _options;
    private CancellationTokenSource? _stopCancellation;
    private bool _isRunning;
    private bool _isDisposed;
    private int _messageId;
    private string? _haVersion;
    private IHaConnection? _connection;
    private TaskCompletionSource _connectionWaiter = new(TaskCreationOptions.RunContinuationsAsynchronously);
    private event Func<HaMessage, bool>? MessageReceived;

    private readonly ConcurrentDictionary<int, string?> _eventSubscriptions = new();
    private readonly ConcurrentDictionary<int, HaTrigger> _triggerSubscriptions = new();
    private readonly ConcurrentDictionary<int, bool> _supervisorSubscriptions = new();
    private readonly ConcurrentDictionary<int, string> _mqttSubscriptions = new();
    private readonly ConcurrentDictionary<int, (DateTime, DateTime?, IEnumerable<string>?, IEnumerable<string>?)> _logbookSubscriptions = new();

    private readonly ConcurrentDictionary<int, int> _subscriptionMappingOldNew = new();
    private readonly ConcurrentDictionary<int, int> _subscriptionMappingNewOld = new();

    private void ThrowIfDisposed()
    {
        if (_isDisposed)
            throw new ObjectDisposedException(nameof(HaClient));
    }

    private void ThrowIfRunning(bool running)
    {
        if (_isRunning == running)
            throw new InvalidOperationException("Home Assistant Client is already " + (running ? "running" : "stopped"));
    }

    private void ThrowIfConnected(bool connected)
    {
        if (IsConnected == connected)
            throw new InvalidOperationException("Cannot perform this operation while " + (connected ? "connected" : "disconnected"));
    }

    private static void ThrowIfError(HaMessage? message)
    {
        if (message == null)
            throw new InvalidDataException("Empty message received");

        if (!message.Success.HasValue && message.Type != "pong")
            throw new InvalidDataException("Missing result from message");

        if (!message.Success!.Value)
        {
            if (message.Error != null)
                throw new InvalidOperationException($"Response error: {message.Error.Message} ({message.Error.Code})");
            else
                throw new InvalidOperationException($"Response error");
        }
    }

    private async Task RestoreSubscriptions()
    {
        _subscriptionMappingNewOld.Clear();
        _subscriptionMappingOldNew.Clear();
        foreach (var subscription in _eventSubscriptions)
        {            
            await SubscribeEventsInternalAsync(subscription.Value, subscription.Key, _stopCancellation!.Token)
                .ConfigureAwait(false);
        }

        foreach (var subscription in _triggerSubscriptions)
        {
            await SubscribeTriggerInternalAsync(subscription.Value, subscription.Key, _stopCancellation!.Token)
                .ConfigureAwait(false);
        }

        foreach (var subscription in _supervisorSubscriptions)
        {
            await SubscribeSupervisorInternalAsync(subscription.Key, _stopCancellation!.Token)
                .ConfigureAwait(false);
        }

        foreach (var subscription in _mqttSubscriptions)
        {
            await SubscribeMqttInternalAsync(subscription.Value, subscription.Key, _stopCancellation!.Token)
                .ConfigureAwait(false);
        }

        foreach (var subscription in _logbookSubscriptions)
        {
            await SubscribeLogbookInternalAsync(subscription.Value.Item1, subscription.Value.Item2, 
                subscription.Value.Item3, subscription.Value.Item4, subscription.Key, _stopCancellation!.Token)
                .ConfigureAwait(false);
        }
    }

    private async Task<int> SubscribeEventsInternalAsync(string? eventType, int oldSubscriptionId, CancellationToken cancellationToken)
    {
        var json = new JsonObject
        {
            ["type"] = "subscribe_events"
        };
        await SendCommandAsync<object>(json, cancellationToken).ConfigureAwait(false);

        var newId = json["id"]?.AsValue()?.GetValue<int>() ?? 0;

        if (oldSubscriptionId >= 0)
        {
            _subscriptionMappingOldNew[oldSubscriptionId] = newId;
            _subscriptionMappingNewOld[newId] = oldSubscriptionId;
        }
        else
        {
            _eventSubscriptions[newId] = eventType;
            _subscriptionMappingOldNew[newId] = newId;
            _subscriptionMappingNewOld[newId] = newId;
        }

        return newId;
    }

    private async Task<int> SubscribeTriggerInternalAsync(HaTrigger trigger, int oldSubscriptionId, CancellationToken cancellationToken)
    {
        var json = new JsonObject
        {
            ["type"] = "subscribe_trigger"
        };
        await SendCommandAsync<Object>(json, cancellationToken).ConfigureAwait(false);

        var newId = json["id"]?.AsValue()?.GetValue<int>() ?? 0;

        _triggerSubscriptions[newId] = trigger;

        if (oldSubscriptionId >= 0)
        {
            _subscriptionMappingOldNew[oldSubscriptionId] = newId;
            _subscriptionMappingNewOld[newId] = oldSubscriptionId;
        }
        else
        {
            _triggerSubscriptions[newId] = trigger;
            _subscriptionMappingOldNew[newId] = newId;
            _subscriptionMappingNewOld[newId] = newId;
        }

        return newId;
    }

    private async Task<int> SubscribeSupervisorInternalAsync(int oldSubscriptionId, CancellationToken cancellationToken)
    {
        var json = new JsonObject
        {
            ["type"] = "supervisor/subscribe"
        };
        await SendCommandAsync<object>(json, cancellationToken).ConfigureAwait(false);

        var newId = json["id"]?.AsValue()?.GetValue<int>() ?? 0;

        if (oldSubscriptionId >= 0)
        {
            _subscriptionMappingOldNew[oldSubscriptionId] = newId;
            _subscriptionMappingNewOld[newId] = oldSubscriptionId;
        }
        else
        {
            _supervisorSubscriptions[newId] = true;
            _subscriptionMappingOldNew[newId] = newId;
            _subscriptionMappingNewOld[newId] = newId;
        }

        return newId;
    }

    private async Task<int> SubscribeMqttInternalAsync(string topic, int oldSubscriptionId, CancellationToken cancellationToken)
    {
        var json = new JsonObject
        {
            ["type"] = "mqtt/subscribe",
            ["topic"] = topic
        };
        await SendCommandAsync<object>(json, cancellationToken).ConfigureAwait(false);

        var newId = json["id"]?.AsValue()?.GetValue<int>() ?? 0;

        if (oldSubscriptionId >= 0)
        {
            _subscriptionMappingOldNew[oldSubscriptionId] = newId;
            _subscriptionMappingNewOld[newId] = oldSubscriptionId;
        }
        else
        {
            _mqttSubscriptions[newId] = topic;
            _subscriptionMappingOldNew[newId] = newId;
            _subscriptionMappingNewOld[newId] = newId;
        }

        return newId;
    }

    private async Task<int> SubscribeLogbookInternalAsync(DateTime startTime, DateTime? endTime, 
        IEnumerable<string>? entityIds, IEnumerable<string>? deviceIds, int oldSubscriptionId,
        CancellationToken cancellationToken)
    {
        var obj = new
        {
            Type = "logbook/event_stream",
            startTime,
            endTime,
            entityIds = entityIds?.ToArray(),
            deviceIds = deviceIds?.ToArray(),
        };

        var json = obj.ToJsonObject();

        await SendCommandAsync<object>(json!, cancellationToken).ConfigureAwait(false);

        var newId = json!["id"]?.AsValue()?.GetValue<int>() ?? 0;

        if (oldSubscriptionId >= 0)
        {
            _subscriptionMappingOldNew[oldSubscriptionId] = newId;
            _subscriptionMappingNewOld[newId] = oldSubscriptionId;
        }
        else
        {
            _logbookSubscriptions[newId] = (startTime, endTime, entityIds, deviceIds);
            _subscriptionMappingOldNew[newId] = newId;
            _subscriptionMappingNewOld[newId] = newId;
        }

        return newId;
    }

    private async Task ConnectAsync()
    {
        _connection?.Dispose();
        _connection = new HaConnection();
        using var timeoutCancellation = new CancellationTokenSource(_options!.ConnectTimeout);
        using var combinedCancellation = CancellationTokenSource.CreateLinkedTokenSource(_stopCancellation!.Token, timeoutCancellation.Token);
        await _connection.ConnectAsync(_options.Uri, combinedCancellation.Token).ConfigureAwait(false);

        var welcome = await ReceiveRawAsync<HaMessage>().ConfigureAwait(false);
        if (welcome?.Type != "auth_required")
            throw new InvalidDataException($"Unexpected message type: '{welcome?.Type}'");

        await SendRawAsync(new
        {
            type = "auth",
            access_token = _options!.Token
        }).ConfigureAwait(false);
        var authResponse = await ReceiveRawAsync<HaMessage>().ConfigureAwait(false);
        if (authResponse?.Type == "auth_invalid")
            throw new UnauthorizedAccessException(authResponse.Message);
        if (authResponse?.Type != "auth_ok")
            throw new InvalidDataException($"Unexpected message type: '{authResponse?.Type}'");

        _haVersion = authResponse!.HaVersion;
    }

    private async Task RunAsync()
    {

        bool reconnect = true;
        HaDisconnectReason disconnectReason = HaDisconnectReason.ConnectionClosed;
        Exception? error = null;
        bool retry = false;
        var reconnectDelay = _options!.ReconnectMinTimeout;
        int attempt = 1;
        while (reconnect && !_stopCancellation!.IsCancellationRequested)
        {
            try
            {
                Connecting?.Invoke(this, new HaConnectingEventArgs(attempt));
                disconnectReason = HaDisconnectReason.ConnectionClosed;
                error = null;
                if (retry)
                    await Task.Delay(reconnectDelay, _stopCancellation.Token).ConfigureAwait(false);
                retry = true;
                await ConnectAsync().ConfigureAwait(false);
                reconnectDelay = _options!.ReconnectMinTimeout;
                attempt = 1;
                Connected?.Invoke(this, new HaConnectedEventArgs(_haVersion));
                _ = RestoreSubscriptions();
                _connectionWaiter.TrySetResult();

                while (!_stopCancellation!.IsCancellationRequested && IsConnected)
                {
                    var message = await _connection!.ReceiveAsync<HaMessage>(_stopCancellation.Token).ConfigureAwait(false);

                    if (message == null)
                        break;

                    bool dispatch = true;
                    if (MessageReceived != null)
                        dispatch = MessageReceived.Invoke(message);

                    if (dispatch)
                    {
                        _subscriptionMappingNewOld.TryGetValue(message.Id ?? 0, out var newId);
                        if (message.Event != null)
                            EventReceived?.Invoke(this, new HaEventEventArgs(message.Event, newId));
                    }

                }
            }
            catch (OperationCanceledException ex) when (ex.CancellationToken == _stopCancellation.Token)
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

            if (error != null)
            {
                reconnectDelay = reconnectDelay.Add(TimeSpan.FromSeconds(1));
                if (reconnectDelay > _options!.ReconnectMaxTimeout)
                    reconnectDelay = _options!.ReconnectMaxTimeout;
                attempt++;
            }
            _connectionWaiter.TrySetResult();
            if (IsConnected)
                await _connection!.CloseAsync(_stopCancellation.Token).ConfigureAwait(false);
            _connection?.Dispose();

            if (Disconnected != null)
            {
                var args = new HaDisconnectedEventArgs(reconnect, disconnectReason, error);
                Disconnected.Invoke(this, args);
                if (!_stopCancellation.IsCancellationRequested)
                    reconnect = args.Reconnect;
            }            
        }

        _isRunning = false;
        if (!_stopCancellation!.IsCancellationRequested)
            _stopCancellation.Cancel();


    }

    public bool IsConnected
        => _connection != null && _connection.IsConnected;
    public bool IsRunning => _isRunning;


    public event EventHandler<HaConnectedEventArgs>? Connected;
    public event EventHandler<HaDisconnectedEventArgs>? Disconnected;
    public event EventHandler<HaConnectingEventArgs>? Connecting;
    public event EventHandler<HaEventEventArgs>? EventReceived;
    
    public Task<int> SubscribeToEventsAsync(string? eventType, CancellationToken cancellationToken = default)
    {
        return SubscribeEventsInternalAsync(eventType, -1, cancellationToken);
    }
    
    public Task<int> SubscribeToTriggerAsync(HaTrigger trigger, CancellationToken cancellationToken = default)
    {
        return SubscribeTriggerInternalAsync(trigger, -1, cancellationToken);
    }

    public Task<int> SubscribeToSupervisorAsync(CancellationToken cancellationToken = default)
    {
        return SubscribeSupervisorInternalAsync(-1, cancellationToken);
    }

    public Task<int> SubscribeToMqttAsync(string topic, CancellationToken cancellationToken = default)
    {
        return SubscribeMqttInternalAsync(topic, -1, cancellationToken);
    }

    public Task<int> SubscribeToLogbookAsync(DateTime startTime, DateTime? endTime = default,
        IEnumerable<string>? entityIds = default, IEnumerable<string>? deviceIds = default,
        CancellationToken cancellationToken = default)
    {
        return SubscribeLogbookInternalAsync(startTime, endTime, entityIds, deviceIds, -1, cancellationToken);
    }

    public Task UnsubscribeAsync(int subscription, CancellationToken cancellationToken = default)
    {
        if (_subscriptionMappingOldNew.TryGetValue(subscription, out var lastSubscriptionId))
            subscription = lastSubscriptionId;

        return SendCommandAsync<Object>(new
        {
            type = "unsubscribe_events",
            subscription
        }, cancellationToken);
    }

    public Task StartAsync(HaClientOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ThrowIfDisposed();
        ThrowIfRunning(true);

        _isRunning = true;
        this._options = options;
        _stopCancellation = new CancellationTokenSource();
        _ = RunAsync();
        _connectionWaiter = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

        return _connectionWaiter.Task;

    }

    public Task StopAsync()
    {
        ThrowIfDisposed();
        ThrowIfRunning(false);

        _eventSubscriptions.Clear();
        _triggerSubscriptions.Clear();
        _supervisorSubscriptions.Clear();
        _mqttSubscriptions.Clear();
        _subscriptionMappingOldNew.Clear();
        _subscriptionMappingNewOld.Clear();
        _logbookSubscriptions.Clear();

        _isRunning = false;

        _stopCancellation?.Cancel();
        _connection?.Dispose();
        _stopCancellation?.Dispose();

        return Task.CompletedTask;
    }


    Task SendRawAsync<T>(T value)
    {
        using var timeoutCancellation = new CancellationTokenSource(_options!.SendTimeout);
        using var combinedCancellation = CancellationTokenSource.CreateLinkedTokenSource(_stopCancellation!.Token, timeoutCancellation.Token);
        return _connection!.SendAsync(value, combinedCancellation.Token);
    }

    Task<TResult?> ReceiveRawAsync<TResult>()
    {
        using var timeoutCancellation = new CancellationTokenSource(_options!.ReceiveTimeout);
        using var combinedCancellation = CancellationTokenSource.CreateLinkedTokenSource(_stopCancellation!.Token, timeoutCancellation.Token);
        return _connection!.ReceiveAsync<TResult>(combinedCancellation.Token);
    }

   

    public async Task<TResult?> SendCommandAsync<TResult>(object command, CancellationToken cancellationToken)
    {
        ThrowIfDisposed();
        ThrowIfConnected(false);
        ArgumentNullException.ThrowIfNull(command);

        var commandId = Interlocked.Increment(ref _messageId);

        var jsonObject = command.ToJsonObjectWithId(commandId);
        if (!jsonObject.ContainsKey("type"))
            throw new ArgumentException("Command without type.", nameof(command));

        var responseWaiter = new TaskCompletionSource<HaMessage?>(TaskCreationOptions.RunContinuationsAsynchronously);
        var responseHandler = new Func<HaMessage, bool>(m =>
        {
            
            if (m.Id == commandId)
            {
                responseWaiter.TrySetResult(m);
                return false;
            }
            return true;
        });

        MessageReceived += responseHandler;
        try
        {
            await SendRawAsync((object)jsonObject).ConfigureAwait(false);

            using var timeoutCancellation = new CancellationTokenSource(_options!.ReceiveTimeout);
            using var combinedCancellation = CancellationTokenSource.CreateLinkedTokenSource(_stopCancellation!.Token,
                cancellationToken, timeoutCancellation.Token);

            var message = await responseWaiter.Task.WaitAsync(combinedCancellation.Token).ConfigureAwait(false);

            ThrowIfError(message);

            if (message!.Result.HasValue)
            {
                string s = message.Result.Value.ToString();
            }

            if (message!.Result.HasValue)
                return message.Result.Value.Deserialize<TResult>(HaTools.DefaultJsonSerializerOptions);
            else
                return default;
        }
        finally
        {
            MessageReceived -= responseHandler;
        }
    }

    

    public void Dispose()
    {
        if (!_isDisposed)
        {
            if (IsRunning)
                _ = StopAsync();
            _isDisposed = true;
            Connected = null;
            Connecting = null;
            Disconnected = null;
            MessageReceived = null;                        
            _connection?.Dispose();
            _stopCancellation?.Dispose();
            _isRunning = false;
        }
    }

}
