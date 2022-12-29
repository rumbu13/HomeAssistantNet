using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaStateTrigger : HaTrigger
{
    [JsonConverter(typeof(JsonOneOrManyConverter<string>))]
    public string[]? EntityId { get; init; }

    public string? Attribute { get; init; }

    [JsonConverter(typeof(JsonAlwaysStringConverter))]
    public string? From { get; init; }

    [JsonConverter(typeof(JsonOneOrManyAlwaysStringConverter))]
    public string[]? To { get; init; }

    [JsonConverter(typeof(JsonDurationConverter))]
    public TimeSpan? For { get; init; }

}
