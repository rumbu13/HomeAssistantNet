using HomeAssistantNet.Api.Internal;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaEntitySelector
{
    public string? Integration { get; init; }
    public string? DeviceClass { get; init; }

    [JsonPropertyName("domain")]
    [JsonConverter(typeof(JsonOneOrManyConverter<string>))]
    public IReadOnlyCollection<string>? Domains { get; init; }

    public IReadOnlyCollection<string>? ExcludeEntities { get; init; }
    public IReadOnlyCollection<string>? IncludeEntities { get; init; }
    public bool? Multiple { get; init; }
}