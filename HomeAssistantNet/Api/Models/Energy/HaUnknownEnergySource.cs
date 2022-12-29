using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaUnknownEnergySource : HaEnergySource
{

    [JsonExtensionData]
    public IDictionary<string, JsonElement>? UnknownData { get; init; }
}
