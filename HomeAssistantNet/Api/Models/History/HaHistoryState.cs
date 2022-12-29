using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaHistoryState
{
    [JsonPropertyName("s")]
    public string? State { get; init; }

    [JsonPropertyName("a")]
    public IDictionary<string, JsonElement>? Attributes { get; init; }

    [JsonPropertyName("lc")]
    [JsonConverter(typeof(JsonTimestampConverter))]
    public DateTime? LastChanged { get; init; }

    [JsonPropertyName("lu")]
    [JsonConverter(typeof(JsonTimestampConverter))]
    public double? LastUpdated { get; init; }


}