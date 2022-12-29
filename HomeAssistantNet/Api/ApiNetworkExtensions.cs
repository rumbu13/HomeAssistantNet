using HomeAssistantNet.Client;

namespace HomeAssistantNet.Api;

public static class ApiNetworkExtensions
{
    
    public static Task<HaNetworkPreferences?> GetNetworkPreferencesAsync(this IHaClient client, 
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaNetworkPreferences>(new
        {
            type = "network"
        }, cancellationToken);

    public static Task<string[]?> UpdateNetworkPreferencesAsync(this IHaClient client,
        IEnumerable<string> configuredAdapters, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<string[]>(new
        {
            type = "network/config",
            config = new
            {
                configuredAdapters = configuredAdapters.ToArray()
            }
        }, cancellationToken);
}