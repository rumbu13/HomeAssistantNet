using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaUnknownEvent : HaEvent
{
    [JsonExtensionData]
    public IDictionary<string, JsonElement>? UnknownData { get; init; }
}