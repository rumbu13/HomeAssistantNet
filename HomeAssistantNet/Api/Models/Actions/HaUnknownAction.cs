using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaUnknowAction : HaAction
{
    [JsonExtensionData]
    public IDictionary<string, JsonElement>? UnknownData { get; init; }
}
