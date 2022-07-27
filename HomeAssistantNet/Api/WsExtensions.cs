using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class WsExtensions
{

    public static Task PingAsync(this IHaWsClient client,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaWsCommand, Object>(new HaWsCommand("ping"), cancellationToken);

    public static Task<IReadOnlyList<HaEntityState>?> GetStatesAsync(this IHaWsClient client,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaWsCommand, IReadOnlyList<HaEntityState>>(new HaWsCommand("get_states"), cancellationToken);

    public static async Task<IReadOnlyList<HaService>?> GetServicesAsync(this IHaWsClient client,
        CancellationToken cancellationToken = default)
    {
        var services = await client.
            SendAsync<HaWsCommand, IDictionary<string, IDictionary<string, HaService>>>(
            new HaWsCommand("get_services"), cancellationToken);

        if (services is not null)
        {
            var list = new List<HaService>();
            foreach (var ds in services)
            {
                foreach (var srv in ds.Value)
                {
                    var haService = srv.Value;
                    haService.ServiceId = srv.Key;
                    haService.Domain = ds.Key;
                    list.Add(haService);
                }

            }

            return list;
        }

        return null;
        
    }

    public static Task<HaConfig?> GetConfigAsync(this IHaWsClient client,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaWsCommand, HaConfig>(new HaWsCommand("get_config"), cancellationToken);

    public static Task<IReadOnlyList<HaArea>?> GetAreasAsync(this IHaWsClient client,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaWsCommand, IReadOnlyList<HaArea>>(new HaWsCommand("config/area_registry/list"), cancellationToken);

    public static Task<HaArea?> CreateAreaAsync(this IHaWsClient client, string name, string? picture = default,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaAreaCreate, HaArea>(new HaAreaCreate(name, picture), cancellationToken);

    public static Task<HaArea?> UpdateAreaAsync(this IHaWsClient client, string areaId, string? name = default, string? picture = default,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaAreaUpdate, HaArea>(new HaAreaUpdate(areaId, name, picture), cancellationToken);

    public static Task DeleteAreaAsync(this IHaWsClient client, string areaId,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaAreaDelete, object>(new HaAreaDelete(areaId), cancellationToken);

    public static Task<IReadOnlyList<HaDevice>?> GetDevicesAsync(this IHaWsClient client,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaWsCommand, IReadOnlyList<HaDevice>>(new HaWsCommand("config/device_registry/list"), cancellationToken);   

    public static Task<HaDevice?> UpdateDeviceAsync(this IHaWsClient client, string deviceId, string? areaId = default, 
        string? nameByUser = default, string? disabledBy = default, CancellationToken cancellationToken = default)
        => client.SendAsync<HaDeviceUpdate, HaDevice>(new HaDeviceUpdate(deviceId, areaId, nameByUser, disabledBy), cancellationToken);

    public static Task RemoveConfigEntryAsync(this IHaWsClient client, string configEntryId, string deviceId,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaDeviceRemoveConfigEntry, object>(new HaDeviceRemoveConfigEntry(configEntryId, deviceId), cancellationToken);

    public static Task<IReadOnlyList<HaEntity>?> GetEntitiesAsync(this IHaWsClient client,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaWsCommand, IReadOnlyList<HaEntity>>(new HaWsCommand("config/entity_registry/list"), cancellationToken);

    public static Task<IReadOnlyList<HaConfigEntry>?> GetConfigEntriesAsync(this IHaWsClient client, string? typeFilter = default, string? domain = default,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaConfigEntryList, IReadOnlyList<HaConfigEntry>>(new HaConfigEntryList(typeFilter, domain), cancellationToken);

    public static Task<HaEntity?> GetEntityExtendedAsync(this IHaWsClient client, string entityId,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaEntityGet, HaEntity>(new HaEntityGet(entityId), cancellationToken);

    public static async Task<IReadOnlyList<HaEntity>?> GetEntitiesExtendedAsync(this IHaWsClient client,
        CancellationToken cancellationToken = default)
    {
        var entities = await client.GetEntitiesAsync(cancellationToken).ConfigureAwait(false);

        var list = new List<HaEntity>();
        foreach (var e in entities!)
        {
            var ent = await client.GetEntityExtendedAsync(e.EntityId!);
            list.Add(ent!);
        }

        return list;
    }

    static Task<IReadOnlyList<T>?> GetListAsync<T>(this IHaWsClient client, string type, CancellationToken cancellationToken = default)
        => client.SendAsync<HaWsCommand, IReadOnlyList<T>>(new HaWsCommand(type), cancellationToken);

    public static Task<IReadOnlyList<HaCounter>?> GetCountersAsync(this IHaWsClient client,
        CancellationToken cancellationToken = default)
       => client.GetListAsync<HaCounter>("counter/list", cancellationToken);

    public static Task<IReadOnlyList<HaInputBoolean>?> GetInputBooleansAsync(this IHaWsClient client,
        CancellationToken cancellationToken = default)
       => client.GetListAsync<HaInputBoolean>("input_boolean/list", cancellationToken);
}