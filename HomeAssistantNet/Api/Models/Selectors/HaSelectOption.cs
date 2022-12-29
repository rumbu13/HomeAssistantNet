using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

[JsonConverter(typeof(JsonHaSelectOptionConverter))]
public sealed record HaSelectOption
{
    public string? Label { get; init; }
    public string? Value { get; init; }
}