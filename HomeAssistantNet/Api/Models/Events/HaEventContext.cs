using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaEventContext
{
    public string? Id { get; init; }
    public string? ParentId { get; init; }

    [JsonConverter(typeof(JsonOneOrManyConverter<string>))]
    public string[]? UserId { get; init; }
}