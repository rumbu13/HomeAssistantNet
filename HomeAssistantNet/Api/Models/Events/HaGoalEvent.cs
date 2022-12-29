namespace HomeAssistantNet.Api;

public sealed record HaGoalEvent : HaStandardEvent
{
    public HaGoalData? Data { get; init; }
}
