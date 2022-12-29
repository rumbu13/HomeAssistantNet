using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaNumericStateTrigger: HaTrigger
{
    public string? EntityId { get; init; }
    public string? Attribute { get; init; }

    [JsonConverter(typeof(JsonDurationConverter))]
    public TimeSpan? For { get; init; }

    public double? Above { get; init; }
    public double? Below { get; init; }
    public string? ValueTemplate { get; init; }
    
}
