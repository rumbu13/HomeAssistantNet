namespace HomeAssistantNet.Api;

public sealed record HaEventVariables
{
    public HaTriggerData? Trigger { get; init; }
}