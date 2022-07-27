using HomeAssistantNet.Client;

namespace HomeAssistantNet.Api;

public static class ApiCoreExtensions
{
    internal static Task<T[]?> GetListAsync<T>(this IHaClient client, string type, 
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaCommand, T[]>(new HaCommand(type), cancellationToken);

    /// <summary>
    /// Sends a "ping" command expecting a "pong" response.
    /// </summary>
    /// <param name="client">The <see cref="IHaClient"/> used to send the command</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for a cancellation request</param>    
    public static Task PingAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaCommand, Object>(new HaCommand("ping"), cancellationToken);

    /// <summary>
    /// Retrieves the current Home Assistant configuration.
    /// </summary>
    /// <param name="client">The <see cref="IHaClient"/> used to send the command</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for a cancellation request</param>    
    /// <returns>A <see cref="HaConfig"/> record representing the  current Home Assistant configuration</returns>
    public static Task<HaConfig?> GetConfigAsync(this IHaClient client,
       CancellationToken cancellationToken = default)
       => client.SendAsync<HaCommand, HaConfig>(new HaCommand("get_config"), cancellationToken);
}