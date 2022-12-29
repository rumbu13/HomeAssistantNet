namespace HomeAssistantNet.Api;

public sealed record HaZoneTrigger : HaTrigger
{
    public string? EntityId { get; init; }
    public string? Zone { get; init; }
    public HaZoneEvent Event { get; init; }
}
