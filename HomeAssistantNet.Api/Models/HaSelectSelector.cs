using HomeAssistantNet.Api.Internal;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaSelectSelector
{
    [JsonConverter(typeof(JsonHaSelectOptionsConverter))]
    public IReadOnlyList<HaSelectOption>? Options { get; set; }

    public bool? Multiple { get; init; }
    public bool? CustomValue { get; init; }
    public string? Mode { get; init; }
}