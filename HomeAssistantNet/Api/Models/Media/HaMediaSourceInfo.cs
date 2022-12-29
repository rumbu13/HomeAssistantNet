namespace HomeAssistantNet.Api;

public sealed record HaMediaSourceInfo
{
    public string? Url { get; init; }
    public string? MimeType { get; init; }
}
