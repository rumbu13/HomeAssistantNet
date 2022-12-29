using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

[JsonConverter(typeof(JsonLovelaceBadgeConfigConverter))]
public sealed record HaLovelaceBadgeConfig
{
    public string? Type { get; init; }

    [JsonExtensionData]
    public IDictionary<string, JsonElement>? Data;

}