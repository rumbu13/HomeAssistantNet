namespace HomeAssistantNet.Client;

public interface IHaWsClient : IDisposable
{
    void Start(HaWsClientOptions options);

    void Stop();
    Task<TResult?> SendAsync<T, TResult>(T value, CancellationToken cancellationToken) where T : HaWsCommand;

    bool IsRunning { get; }
    bool IsConnected { get; }

    event EventHandler<HaWsConnectingEventArgs>? Connecting;
    event EventHandler<HaWsConnectedEventArgs>? Connected;
    event EventHandler<HaWsDisconnectedEventArgs>? Disconnected;
    event EventHandler<HaWsEventEventArgs>? EventReceived;


}
