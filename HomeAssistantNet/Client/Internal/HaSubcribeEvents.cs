using HomeAssistantNet.Client;

namespace HomeAssistantNet.Client.Internal;

internal sealed record HaSubcribeEvents : HaWsCommand
{
    public HaSubcribeEvents(string? eventType = default)
        : base("subscribe_events")
    {
        EventType = eventType;
    }
    public string? EventType { get; init; }
}
