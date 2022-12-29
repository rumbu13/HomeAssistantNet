namespace HomeAssistantNet.Api;

public sealed record HaAreasUpdatedEvent : HaStandardEvent
{
    public HaAreasUpdatedData? Data { get; init; }
}
