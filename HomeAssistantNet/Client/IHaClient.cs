using HomeAssistantNet.Api;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Client;

public interface IHaClient : IDisposable
{
    Task StartAsync(HaClientOptions options);
    Task StopAsync();
    
    Task<TResult?> SendCommandAsync<TResult>(Object value, CancellationToken cancellationToken = default);

    Task<int> SubscribeEventsAsync(string? eventType, CancellationToken cancellationToken = default);   
    Task<int> SubscribeTriggerAsync(HaTrigger trigger, CancellationToken cancellationToken = default);
    Task<int> SubscribeSupervisorAsync(CancellationToken cancellationToken = default);
    Task<int> SubscribeMqttAsync(string topic, CancellationToken cancellationToken = default);
    Task<int> SubscribeLogbookAsync(DateTime startTime, DateTime? endTime = default,
        IEnumerable<string>? entityIds = default, IEnumerable<string>? deviceIds = default,
        CancellationToken cancellationToken = default);

    Task UnsubscribeAsync(int subscription, CancellationToken cancellationToken = default);

    bool IsRunning { get; }
    bool IsConnected { get; }

    event EventHandler<HaEventEventArgs>? EventReceived;
    event EventHandler<HaConnectingEventArgs>? Connecting;
    event EventHandler<HaConnectedEventArgs>? Connected;
    event EventHandler<HaDisconnectedEventArgs>? Disconnected;
    

    public static IHaClient CreateDefault()
    {
        return new HaClient();
    }


    
}
