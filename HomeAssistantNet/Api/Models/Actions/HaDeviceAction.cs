using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaDeviceAction : HaAction
{    
    public string? DeviceId { get; init; }
    public string? Domain { get; init; }
    public string? EntityId { get; init; }
    public string? Type { get; init; }
}
