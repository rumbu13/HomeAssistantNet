namespace HomeAssistantNet.Api;

public sealed record HaIdteckProximityEvent : HaStandardEvent
{
    public HaIdteckProximityData? Data { get; init; }
}
