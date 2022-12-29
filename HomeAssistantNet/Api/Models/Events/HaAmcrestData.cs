using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaAmcrestData
{
    public string? Camera { get; init; }
    public string? Event { get; init; }
    public JsonElement? Payload { get; init; }
}
