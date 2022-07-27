namespace HomeAssistantNet.Api;

public sealed record HaEntityData
{
    public string? Action { get; init; }
    public string? EntityId { get; init; }
    public HaEntity? Changes { get; init; }
}
