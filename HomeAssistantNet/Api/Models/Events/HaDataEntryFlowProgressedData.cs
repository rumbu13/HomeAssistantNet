namespace HomeAssistantNet.Api;

public sealed record HaDataEntryFlowProgressedData
{
    public string? Handler { get; init; }
    public string? FlowId { get; init; }
    public bool? Refresh { get; init; }
}
