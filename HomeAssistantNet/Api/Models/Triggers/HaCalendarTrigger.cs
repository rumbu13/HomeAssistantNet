using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaCalendarTrigger : HaTrigger
{
    public HaCalendarEvent? Event { get; init; }
    public string? EntityId { get; init; }
    public string? Offset { get; init; }
}
