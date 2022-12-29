using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

[JsonConverter(typeof(JsonEnergySourceConverter))]
public abstract record HaEnergySource
{
    public string? Type { get; init; }
}
