using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaService
{
    [JsonIgnore]
    public string? Domain { get; init; }

    [JsonIgnore]
    public string? ServiceId { get; init; }

    public string? Name { get; init; }
    public string? Description { get; init; }
    public IDictionary<string, HaField>? Fields { get; init; }
    public HaTargetSelector? Target { get; init; }
}