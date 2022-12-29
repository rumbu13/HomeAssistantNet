namespace HomeAssistantNet.Api;

public sealed record HaCalendarTriggerData: HaTriggerData
{
    public string? Platform { get; init; }
    public string? Event { get; init; }
    public string? Offset { get; init; }
    public HaCalendarData? CalendarEvent { get; init; }
}
