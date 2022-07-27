namespace HomeAssistantNet.Client;

public interface IHaClient : IDisposable
{
    void Start(HaClientOptions options);

    void Stop();
    Task<TResult?> SendAsync<T, TResult>(T value, CancellationToken cancellationToken) where T : HaCommand;

    bool IsRunning { get; }
    bool IsConnected { get; }

    event EventHandler<HaConnectingEventArgs>? Connecting;
    event EventHandler<HaConnectedEventArgs>? Connected;
    event EventHandler<HaDisconnectedEventArgs>? Disconnected;
    event EventHandler<HaEventEventArgs>? EventReceived;


    Task WaitForConnectionAsync(bool stopOnError, CancellationToken cancellationToken = default);
    
}
