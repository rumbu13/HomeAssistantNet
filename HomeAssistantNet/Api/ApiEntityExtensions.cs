using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiEntityExtensions
{
    public static Task<HaEntity[]?> GetEntitiesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaCommand, HaEntity[]>(new HaCommand("config/entity_registry/list"), cancellationToken);

    public static Task<HaEntity?> GetEntityExtendedAsync(this IHaClient client, string entityId,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaEntityGet, HaEntity>(new HaEntityGet(entityId), cancellationToken);

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

    public static Task<IReadOnlyDictionary<string, HaEntitySource>?> GetEntitySourcesAsync(this IHaClient client,
       IEnumerable<string>? filter = default, CancellationToken cancellationToken = default)
       => client.SendAsync<HaEntityGetSource, IReadOnlyDictionary<string, HaEntitySource>>(
           new HaEntityGetSource(filter), cancellationToken);

    public async static Task<HaEntitySource?> GetEntitySourceAsync(this IHaClient client,
       string entityId, CancellationToken cancellationToken = default)
    {
        var result = await client.GetEntitySourcesAsync(new string[] { entityId }, cancellationToken);
        return result?.Values.FirstOrDefault();
    }

}

