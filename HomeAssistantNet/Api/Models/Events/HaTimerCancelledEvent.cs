namespace HomeAssistantNet.Api;

public sealed record HaTimerCancelledEvent : HaStandardEvent
{
    public HaTimerData? Data { get; init; }
}
