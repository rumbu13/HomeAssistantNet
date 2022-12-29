namespace HomeAssistantNet.Api;

public sealed record HaTagScannedEvent : HaStandardEvent
{
    public HaTagScannedData? Data { get; init; }
}
