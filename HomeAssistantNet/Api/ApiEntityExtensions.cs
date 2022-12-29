using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiEntityExtensions
{
    public static Task<HaEntity[]?> GetEntitiesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.GetListAsync<HaEntity>("config/entity_registry/list", cancellationToken);

    public static Task<HaEntity?> GetEntityExtendedAsync(this IHaClient client, string entityId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaEntity>(new { Type = "config/entity_registry/get", entityId }, cancellationToken);

    public static async Task<HaEntity[]?> GetEntitiesExtendedAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
    {
        var entities = await client.GetEntitiesAsync(cancellationToken).ConfigureAwait(false);

        var list = new List<HaEntity>();
        foreach (var e in entities!)
        {
            var ent = await client.GetEntityExtendedAsync(e.EntityId!, cancellationToken);
            list.Add(ent!);
        }

        return list.ToArray();
    }

    public static Task<IDictionary<string, HaEntitySource>?> GetEntitySourcesAsync(this IHaClient client,
       IEnumerable<string>? filter = default, CancellationToken cancellationToken = default)    
        => client.SendCommandAsync<IDictionary<string, HaEntitySource>>(
           new { type = "entity/source", filter = filter?.ToArray() }, cancellationToken);

}

