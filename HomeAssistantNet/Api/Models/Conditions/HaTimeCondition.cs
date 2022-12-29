using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaTimeCondition : HaCondition
{
    public string? After { get; init; }
    public string? Before { get; init; }

    [JsonConverter(typeof(JsonOneOrManyConverter<string>))]
    public string[]? Weekday { get; init; }
}
