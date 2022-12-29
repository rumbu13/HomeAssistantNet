namespace HomeAssistantNet.Client.Internal;

internal interface IHaConnection : IDisposable
{
    Task ConnectAsync(Uri uri, CancellationToken cancellationToken);
    Task CloseAsync(CancellationToken cancellationToken);
    Task SendAsync<T>(T message, CancellationToken cancellationToken);
    Task<T?> ReceiveAsync<T>(CancellationToken cancellationToken);

    bool IsConnected { get; }


}
