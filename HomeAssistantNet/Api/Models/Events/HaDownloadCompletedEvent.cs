namespace HomeAssistantNet.Api;

public sealed record HaDownloadCompletedEvent : HaStandardEvent
{
    public HaDownloadData? Data { get; init; }
}
