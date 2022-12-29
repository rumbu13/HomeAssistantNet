namespace HomeAssistantNet.Api;

public sealed record HaFolderWatcherEvent : HaStandardEvent
{
    public HaFolderWatcherData? Data { get; init; }
}
