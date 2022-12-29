using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiMqttExtensions
{

    public static Task<HaMqttDeviceInfo?> GetMqttDeviceInfoAsync(this IHaClient client, string deviceId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaMqttDeviceInfo>(new 
        { 
            type = "mqtt/device/debug_info", 
            deviceId
        }, cancellationToken);




}