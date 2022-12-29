namespace HomeAssistantNet.Api;

public sealed record HaFolderWatcherData
{
    public string? EventType { get; init; }
    public string? Path { get; init; }
    public string? File { get; init; }
    public string? Folder { get; init; }
    public string? DestPath { get; init; }
    public string? DestFile { get; init; }
    public string? DestFolder { get; init; }
}
