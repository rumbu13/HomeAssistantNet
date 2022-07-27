using HomeAssistantNet.Client;
using System.Collections.Specialized;
using System.Text;
using System.Text.Json;

namespace HomeAssistantNet.Api;

public static class WsExtensions
{

    /// <summary>
    /// Sends a ping command
    /// </summary>
    /// <param name="client"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task PingAsync(this IHaWsClient client,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaWsCommand, Object>(new HaWsCommand("ping"), cancellationToken);


   
    /// <summary>
    /// Get all entity states
    /// </summary>
    /// <param name="client"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<IReadOnlyList<HaEntityState>?> GetStatesAsync(this IHaWsClient client,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaWsCommand, IReadOnlyList<HaEntityState>>(new HaWsCommand("get_states"), cancellationToken);


    /// <summary>
    /// Gets all available services
    /// </summary>
    /// <param name="client"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<IReadOnlyDictionary<string, IReadOnlyDictionary<string, HaService>>?> GetServicesAsync(this IHaWsClient client,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaWsCommand, IReadOnlyDictionary<string, IReadOnlyDictionary<string, HaService>>>(new HaWsCommand("get_services"), cancellationToken);


    /// <summary>
    /// Gets the current configuration
    /// </summary>
    /// <param name="client"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<HaConfig?> GetConfigAsync(this IHaWsClient client,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaWsCommand, HaConfig>(new HaWsCommand("get_config"), cancellationToken);

}