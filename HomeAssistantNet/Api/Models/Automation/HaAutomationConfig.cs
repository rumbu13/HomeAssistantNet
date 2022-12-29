using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaAutomationConfig
{
    public string? Id { get; init; }
    public string? Alias { get; init; }
    public string? Description { get; init; }

    [JsonConverter(typeof(JsonOneOrManyConverter<HaTrigger>))]
    public HaTrigger[]? Trigger { get; init; }

    [JsonConverter(typeof(JsonOneOrManyConverter<HaCondition>))]
    public HaCondition[]? Condition { get; init; }

    public HaAutomationMode? Mode { get; init; }
    public double? Max { get; init; }
    public HaMaxExceeded? MaxExceeded { get; init; }
    public IDictionary<string, JsonElement>? Variables { get; init; }
}
