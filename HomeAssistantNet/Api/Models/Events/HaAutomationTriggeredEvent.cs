namespace HomeAssistantNet.Api;

public sealed record HaAutomationTriggeredEvent : HaStandardEvent
{
    public HaAutomationTriggeredData? Data { get; init; }
}
