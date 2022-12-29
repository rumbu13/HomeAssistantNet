using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaUnknownStandardEvent : HaStandardEvent
{
   
    public JsonElement? Data { get; init; }
    public IDictionary<string, JsonElement>? Variables { get; init; }

    [JsonExtensionData]
    public IDictionary<string, JsonElement>? UnknownData { get; init; }
}