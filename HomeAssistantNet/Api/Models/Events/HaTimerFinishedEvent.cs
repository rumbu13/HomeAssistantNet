namespace HomeAssistantNet.Api;

public sealed record HaTimerFinishedEvent : HaStandardEvent
{
    public HaTimerData? Data { get; init; }
}
