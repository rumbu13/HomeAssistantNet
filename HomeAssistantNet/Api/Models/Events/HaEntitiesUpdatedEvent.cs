namespace HomeAssistantNet.Api;

public sealed record HaEntitiesUpdatedEvent : HaStandardEvent
{
    public HaEntitiesUpdatedData? Data { get; init; }
}
