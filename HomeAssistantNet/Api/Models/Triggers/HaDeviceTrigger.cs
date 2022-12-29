using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaDeviceTrigger : HaTrigger
{
    public string? Alias { get; init; }
    public string? DeviceId { get; init; }
    public string? Domain { get; init; }
    public string? EntityId { get; init; }
    public string? Type { get; init; }
    public string? Subtype { get; init; }
    public string? Event { get; init; }
    public HaDeviceMetadata? Metadata { get; init; }
    public string? DiscoveryId { get; private set; }
}
