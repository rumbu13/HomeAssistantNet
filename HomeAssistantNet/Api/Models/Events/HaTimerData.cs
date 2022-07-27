namespace HomeAssistantNet.Api;

public sealed record HaTimerData
{
    public string? EntityId { get; init; }
    public DateTime? FinishedAt { get; init; }
}
