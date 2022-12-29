namespace HomeAssistantNet.Api;

public sealed record HaRepairsUpdatedEvent : HaStandardEvent
{
    public HaRepairsUpdatedData? Data { get; init; }
}
