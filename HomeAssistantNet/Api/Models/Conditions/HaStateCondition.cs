using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaStateCondition : HaCondition
{
    public string? EntityId { get; init; }
    public string? Attribute { get; init; }

    [JsonConverter(typeof(JsonOneOrManyAlwaysStringConverter))]
    public string[]? State { get; init; }

    [JsonConverter(typeof(JsonDurationConverter))]
    public TimeSpan? For { get; init; }

}
