namespace HomeAssistantNet.Api;

public sealed record HaFlowProgressData
{
    public string? Handler { get; init; }
    public string? FlowId { get; init; }
    public bool? Refresh { get; init; }
}
