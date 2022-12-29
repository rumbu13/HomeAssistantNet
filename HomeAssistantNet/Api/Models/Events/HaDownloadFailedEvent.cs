namespace HomeAssistantNet.Api;

public sealed record HaDownloadFailedEvent : HaStandardEvent
{
    public HaDownloadData? Data { get; init; }
}
