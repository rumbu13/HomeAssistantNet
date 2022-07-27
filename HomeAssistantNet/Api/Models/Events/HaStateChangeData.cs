namespace HomeAssistantNet.Api;

public sealed record HaStateChangeData
{
    public string? EntityId { get; init; }
    public HaEntityState? NewState { get; init; }
    public HaEntityState? OldState { get; init; }
}
