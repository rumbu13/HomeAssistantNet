using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiHardwareExtensions
{
    public static Task<HaHardwareInfo?> GetHardwareInfoAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaHardwareInfo>(new
        { 
            Type = "hardware/info" 
        }, cancellationToken);


}

