namespace HomeAssistantNet.Api;

public sealed record HaDownloadData
{
    public string? Url { get; init; }
    public string? Filename { get; init; }
}
