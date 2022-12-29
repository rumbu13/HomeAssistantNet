using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public record HaDeviceCondition : HaCondition
{
    public string? DeviceId { get; init; }
    public string? Domain { get; init; }
    public string? EntityId { get; init; }
    public string? Type { get; init; }
    public string? Subtype { get; init; }
    public string? Event { get; init; }
    public HaDeviceMetadata? Metadata { get; init; }
}
