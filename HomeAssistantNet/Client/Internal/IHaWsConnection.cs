
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Client.Internal;

internal interface IHaWsConnection : IDisposable
{
    Task StartAsync(Uri uri, CancellationToken cancellationToken);
    Task StopAsync(CancellationToken cancellationToken);
    Task SendAsync<T>(T message, CancellationToken cancellationToken);
    Task<T?> ReceiveAsync<T>(CancellationToken cancellationToken);

    bool IsConnected { get; }


}
