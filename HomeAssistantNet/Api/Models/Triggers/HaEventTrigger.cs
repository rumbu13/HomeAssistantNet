using HomeAssistantNet.Client;
using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaEventTrigger: HaTrigger
{
    public string? EventType { get; init; }
    public JsonElement? EventData { get; init; }
    public HaEventContext? Context { get; init; }
}
