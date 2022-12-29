namespace HomeAssistantNet.Api;

public sealed record HaDoorbirdData
{
    public DateTime? Timestamp { get; init; }
    public string? LiveVideoUrl { get; init; }
    public string? LiveImageUrl { get; init; }
    public string? RstpLiveVideoUrl { get; init; }
    public string? Html5ViewerUrl { get; init; }
}