namespace HomeAssistantNet.Api;

public sealed record HaTimerStartedEvent : HaStandardEvent
{
    public HaTimerData? Data { get; init; }
}
