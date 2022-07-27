using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiConfigEntryExtensions
{
    public static Task<HaConfigEntry[]?> GetConfigEntriesAsync(this IHaClient client, string? typeFilter = default, 
        string? domain = default, CancellationToken cancellationToken = default)
        => client.SendAsync<HaConfigEntryList, HaConfigEntry[]>(new HaConfigEntryList(typeFilter, domain), cancellationToken);

}