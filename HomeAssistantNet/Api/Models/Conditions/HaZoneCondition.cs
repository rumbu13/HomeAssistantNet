namespace HomeAssistantNet.Api;

public sealed record HaZoneCondition : HaCondition
{
    public string? EntityId { get; init; }
    public string? Zone { get; init; }
}
