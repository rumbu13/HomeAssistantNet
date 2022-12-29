using HomeAssistantNet.Client;

namespace HomeAssistantNet.Api;

public static class ApiCoreExtensions
{
    internal static Task<T[]?> GetListAsync<T>(this IHaClient client, string type, 
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<T[]>(new { type }, cancellationToken);

    public static Task PingAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<Object>(new { type = "ping" }, cancellationToken);

    
}