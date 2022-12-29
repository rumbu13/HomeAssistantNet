using HomeAssistantNet.Client;

namespace HomeAssistantNet.Api;

public static class ApiAreaExtensions
{
  
    public static Task<HaArea[]?> GetAreasAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaArea[]>(new { type = "config/area_registry/list" }, cancellationToken);

    public static Task<HaArea?> CreateAreaAsync(this IHaClient client, string name, string? picture = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaArea>(new { type = "config/area_registry/create", name, picture }, cancellationToken);

    public static Task<HaArea?> UpdateAreaAsync(this IHaClient client, string areaId, string? name = default, string? picture = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaArea>(new { type = "config/area_registry/update", areaId, name, picture }, cancellationToken);

    public static Task DeleteAreaAsync(this IHaClient client, string areaId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new { type = "config/area_registry/delete", areaId }, cancellationToken);

  
}