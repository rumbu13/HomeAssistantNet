namespace HomeAssistantNet.Api;

public sealed record HaComponentLoadedEvent : HaStandardEvent
{
    public HaComponentLoadedData? Data { get; init; }
}
