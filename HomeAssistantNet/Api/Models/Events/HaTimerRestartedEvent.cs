namespace HomeAssistantNet.Api;

public sealed record HaTimerRestartedEvent : HaStandardEvent
{
    public HaTimerData? Data { get; init; }
}
