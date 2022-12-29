namespace HomeAssistantNet.Api;

public sealed record HaScriptStartedEvent : HaStandardEvent
{
    public HaScriptStartedData? Data { get; init; }
}
