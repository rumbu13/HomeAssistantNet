using System.Text.Json;

namespace HomeAssistantNet.Client;

public sealed record HaEvent
{
    public JsonElement? Data { get; init; }
    public string? EventType { get; init; }
    public DateTime? TimeFired { get; init; }
    public string? Origin { get; init; }
    public HaContext? Context { get; init; }

}