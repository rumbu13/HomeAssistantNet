using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiAreaExtensions
{
  
    public static Task<HaArea[]?> GetAreasAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaCommand, HaArea[]>(new HaCommand("config/area_registry/list"), cancellationToken);

    public static Task<HaArea?> CreateAreaAsync(this IHaClient client, string name, string? picture = default,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaAreaCreate, HaArea>(new HaAreaCreate(name, picture), cancellationToken);

    public static Task<HaArea?> UpdateAreaAsync(this IHaClient client, string areaId, string? name = default, string? picture = default,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaAreaUpdate, HaArea>(new HaAreaUpdate(areaId, name, picture), cancellationToken);

    public static Task DeleteAreaAsync(this IHaClient client, string areaId,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaAreaDelete, object>(new HaAreaDelete(areaId), cancellationToken);

  
}