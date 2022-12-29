namespace HomeAssistantNet.Api;

public sealed record HaTimerPausedEvent : HaStandardEvent
{
    public HaTimerData? Data { get; init; }
}
