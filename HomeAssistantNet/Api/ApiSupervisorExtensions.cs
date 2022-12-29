using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiSupervisiorExtensions
{
    public static Task<T?> CallSupervisorApiAsync<T>(this IHaClient client, string endpoint, string method, 
        object? data = default, double? timeout = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<T>(new
        {
            type = "supervisor/api",
            endpoint,
            method,
            data,
            timeout
        }, cancellationToken);

    public static Task FireSupervisorEventAsync(this IHaClient client, object data,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "supervisor/event",
            data
        }, cancellationToken);
}