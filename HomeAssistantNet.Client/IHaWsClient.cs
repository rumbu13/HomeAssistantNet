namespace HomeAssistantNet.Client;

public interface IHaWsClient : IDisposable
{
    /// <summary>
    /// Initiates and maintains a websocket connection to Home Assistant
    /// </summary>
    /// <param name="options"></param>
    void Start(HaWsClientOptions options);

    /// <summary>
    /// Stops the current websocket connection
    /// </summary>
    void Stop();

    /// <summary>
    /// Sends specified value over websocket connection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="value"></param>
    /// <returns>Websocket response</returns>
    Task<TResult?> SendAsync<T, TResult>(T value, CancellationToken cancellationToken) where T : HaWsCommand;


    /// <summary>
    /// True if the current connection is maintained
    /// </summary>
    bool IsRunning { get; }
    /// <summary>
    /// True if the current connection is available
    /// </summary>
    bool IsConnected { get; }


    event EventHandler<HaWsConnectedEventArgs>? Connected;
    event EventHandler<HaWsDisconnectedEventArgs>? Disconnected;
    event EventHandler<HaWsMessageEventArgs>? MessageReceived;
    event EventHandler<HaWsEventEventArgs>? EventReceived;

    
}
