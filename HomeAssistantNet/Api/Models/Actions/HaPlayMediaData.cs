namespace HomeAssistantNet.Api;

public sealed record HaPlayMediaData
{
    public string? MediaContentId { get; init; }
    public string? MediaContentType { get; init; }
}