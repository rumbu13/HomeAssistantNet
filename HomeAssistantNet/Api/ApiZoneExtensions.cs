using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiZoneExtensions
{

    public static Task<HaZone[]?> GetZonesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
       => client.GetListAsync<HaZone>("zone/list", cancellationToken);

    public static Task<HaZone?> CreateZoneAsync(this IHaClient client, string name, double latitude, double longitude,
        string? icon = default, double? radius = default, bool? passive = default, 
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaZone>(new
        {
            type = "zone/create",
            name,
            icon,
            latitude,
            longitude,
            radius,
            passive
        }, cancellationToken);

    public static Task<HaZone?> UpdateZoneAsync(this IHaClient client, string zoneId, string? name = default,
         double? latitude = default, double? longitude = default, string? icon = default, double? radius = default, 
         bool? passive = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaZone>(new
        {
            type = "zone/update",
            zoneId,
            name,
            icon,
            latitude,
            longitude,
            radius,
            passive
        }, cancellationToken);

    public static Task<HaZone?> DeleteZoneAsync(this IHaClient client, string zoneId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaZone>(new
        {
            type = "zone/delete",
            zoneId
        }, cancellationToken);

}