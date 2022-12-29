namespace HomeAssistantNet.Api;

public sealed record HaEntitiesUpdatedData
{
    public string? Action { get; init; }
    public string? EntityId { get; init; }
    public string? OldEntityId { get; init; }
    public HaEntity? Changes { get; init; }
}
