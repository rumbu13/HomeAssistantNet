namespace HomeAssistantNet.Api;

public sealed record HaTriggerEvent : HaStandardEvent
{
    public HaEventVariables? Variables { get; init; }
}
