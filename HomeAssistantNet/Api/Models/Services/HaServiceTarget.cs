using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaServiceTarget
{
    [JsonConverter(typeof(JsonOneOrManyConverter<string>))]
    public string[]? EntityId { get; init; }

    [JsonConverter(typeof(JsonOneOrManyConverter<string>))]
    public string[]? DeviceId { get; set; }

    [JsonConverter(typeof(JsonOneOrManyConverter<string>))]
    public string[]? AreaId { get; set; }
}
