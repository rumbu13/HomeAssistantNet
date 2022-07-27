namespace HomeAssistantNet.Client.Internal;

internal interface IHaConnection : IDisposable
{
    Task StartAsync(Uri uri, CancellationToken cancellationToken);
    Task StopAsync(CancellationToken cancellationToken);
    Task SendAsync<T>(T message, CancellationToken cancellationToken);
    Task<T?> ReceiveAsync<T>(CancellationToken cancellationToken);

    bool IsConnected { get; }


}
