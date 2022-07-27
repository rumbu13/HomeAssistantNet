using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaSelectSelector
{
    [JsonConverter(typeof(JsonHaSelectOptionsConverter))]
    public HaSelectOption[]? Options { get; init; }

    public bool? Multiple { get; init; }
    public bool? CustomValue { get; init; }
    public string? Mode { get; init; }
}