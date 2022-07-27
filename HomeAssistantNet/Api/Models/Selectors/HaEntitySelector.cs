using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaEntitySelector
{
    public string? Integration { get; init; }
    public string? DeviceClass { get; init; }

    [JsonPropertyName("domain")]
    [JsonConverter(typeof(JsonOneOrManyConverter<string>))]
    public string[]? Domains { get; init; }

    public string[]? ExcludeEntities { get; init; }
    public string[]? IncludeEntities { get; init; }
    public bool? Multiple { get; init; }
}