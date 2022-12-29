namespace HomeAssistantNet.Api;

public sealed record HaDataEntryFlowProgressedEvent : HaStandardEvent
{
    public HaDataEntryFlowProgressedData? Data { get; init; }
}
