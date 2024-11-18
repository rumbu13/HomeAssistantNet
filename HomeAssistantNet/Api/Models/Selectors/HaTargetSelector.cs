using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaTargetSelector
{
    [JsonConverter(typeof(JsonOneOrManyConverter<HaEntitySelector>))]
    public HaEntitySelector?[]? Entities { get; init; }
    [JsonConverter(typeof(JsonOneOrManyConverter<HaDeviceSelector>))]
    public HaDeviceSelector?[]? Devices { get; init; }
}