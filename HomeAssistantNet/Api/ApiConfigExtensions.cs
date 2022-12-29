using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiConfigExtensions
{
    public static Task<HaConfigEntry[]?> GetConfigEntriesAsync(this IHaClient client, string? typeFilter = default, 
        string? domain = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaConfigEntry[]>(new { type = "config_entries/get", typeFilter, domain }, cancellationToken);

    public static Task<HaConfigEntryResult?> UpdateConfigEntryAsync(this IHaClient client, string entryId,
        string? title = default, bool? prefDisableNewEntities = default, bool? prefDisablePolling = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaConfigEntryResult>(new 
        { 
            type = "config_entries/update",
            entryId,
            title,
            prefDisableNewEntities,
            prefDisablePolling,
        }, cancellationToken);

    public static Task<HaConfigEntryResult?> DisableConfigEntryAsync(this IHaClient client, string entryId,
       HaDisabledBy disabledBy, CancellationToken cancellationToken = default)
       => client.SendCommandAsync<HaConfigEntryResult>(new
       {
           type = "config_entries/disable",
           entryId,
           disabledBy,
       }, cancellationToken);

    public static Task IgnoreConfigEntryFlowAsync(this IHaClient client, string flowId, string title,
       CancellationToken cancellationToken = default)
       => client.SendCommandAsync<object>(new
       {
           type = "config_entries/ignore_flow",
           flowId,
           title
       }, cancellationToken);

    public static Task<HaConfig?> GetConfigAsync(this IHaClient client, CancellationToken cancellationToken = default)
       => client.SendCommandAsync<HaConfig>(new { type = "get_config" }, cancellationToken);

    public static Task UpdateConfigAsync(this IHaClient client, double? latitude = default, double? longitude = default,
        int? elevation = default, HaUnitSystemType unitSystem = default, string? locationName = default, 
        string? timeZone = default, string? externalUrl = default, string? internalUrl = default, 
        string? currency = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaConfig>(new 
        { 
            type = "config/core/update"
        }, cancellationToken);

    public static Task<HaConfig?> DetectConfigAsync(this IHaClient client, CancellationToken cancellationToken = default)
       => client.SendCommandAsync<HaConfig>(new
       {           
           type = "config/core/detect"
       }, cancellationToken);
}