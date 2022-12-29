namespace HomeAssistantNet.Api;

public sealed record HaZoneTriggerData: HaTriggerData
{
    public string? EntityId { get; init; }
    public HaEntityState? FromState { get; init; }
    public HaEntityState? ToState { get; init; }
    public HaEntityState? Zone { get; init; }
    public string? Event { get; init; }
    public string? Description { get; init; }

}
