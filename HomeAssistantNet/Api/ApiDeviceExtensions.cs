using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiDeviceExtensions
{   
    public static Task<HaDevice[]?> GetDevicesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaDevice[]>(new { Type = "config/device_registry/list" }, cancellationToken);   

    public static Task<HaDevice?> UpdateDeviceAsync(this IHaClient client, string deviceId, string? areaId = default, 
        string? nameByUser = default, string? disabledBy = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaDevice>(new { type = "config/device_registry/update", deviceId, areaId, nameByUser, disabledBy }, 
            cancellationToken);

    public static Task RemoveDeviceConfigEntryAsync(this IHaClient client, string configEntryId, string deviceId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new { type = "config/device_registry/remove_config_entry", configEntryId, deviceId }, 
            cancellationToken);

    public async static Task DeleteDeviceAsync(this IHaClient client, string deviceId,
        CancellationToken cancellationToken = default)
    {
        var devices = await client.GetDevicesAsync(cancellationToken);
        var thisDevice = devices?.FirstOrDefault(d => d.Id == deviceId);

        if (thisDevice is not null && thisDevice.ConfigEntries is not null)
        {
            foreach (var configEntryId in thisDevice.ConfigEntries)
                await client.RemoveDeviceConfigEntryAsync(configEntryId, deviceId, cancellationToken);
        }
    }



}