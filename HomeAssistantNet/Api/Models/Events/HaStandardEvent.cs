using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public abstract record HaStandardEvent : HaEvent
{
    public string? EventType { get; init; }
    public DateTime? TimeFired { get; init; }
    public string? Origin { get; init; }
    public HaEventContext? Context { get; init; }
}