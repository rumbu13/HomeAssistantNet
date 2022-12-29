using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaLovelaceCardConfig
{
    public double? Index { get; init; }
    public double? ViewIndex { get; init; }
    public JsonElement? ViewLayout { get; init; }
    public string? Type { get; init; }

    [JsonExtensionData]
    public IDictionary<string, JsonElement>? Data;

}