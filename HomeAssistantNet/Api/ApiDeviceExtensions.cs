using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiDeviceExtensions
{   
    public static Task<HaDevice[]?> GetDevicesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaCommand, HaDevice[]>(new HaCommand("config/device_registry/list"), cancellationToken);   

    public static Task<HaDevice?> UpdateDeviceAsync(this IHaClient client, string deviceId, string? areaId = default, 
        string? nameByUser = default, string? disabledBy = default, CancellationToken cancellationToken = default)
        => client.SendAsync<HaDeviceUpdate, HaDevice>(new HaDeviceUpdate(deviceId, areaId, nameByUser, disabledBy), 
            cancellationToken);

    public static Task RemoveDeviceConfigEntryAsync(this IHaClient client, string configEntryId, string deviceId,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaDeviceRemoveConfigEntry, object>(new HaDeviceRemoveConfigEntry(configEntryId, deviceId), 
            cancellationToken);

    
}