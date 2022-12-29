namespace HomeAssistantNet.Api;

public sealed record HaStateChangedEvent : HaStandardEvent
{
    public HaStateChangedData? Data { get; init; }
}
