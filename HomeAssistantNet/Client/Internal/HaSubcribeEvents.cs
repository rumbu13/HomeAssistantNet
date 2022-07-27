namespace HomeAssistantNet.Client.Internal;

internal sealed class HaSubcribeEvents : HaCommand
{
    public HaSubcribeEvents(string? eventType = default)
        : base("subscribe_events")
    {
        EventType = eventType;
    }
    public string? EventType { get; init; }
}
