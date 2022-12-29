namespace HomeAssistantNet.Api;

public sealed record HaMediaPlayerItem
{
    public string? Title { get; init; }
    public string? MediaContentType { get; init; }
    public string? MediaContentId { get; init; }
    public string? MediaClass { get; init; }
    public string? ChildrenMediaClass { get; init; }
    public bool? CanPlay { get; init; }
    public bool? CanExpand { get; init; }
    public string? Thumbnail { get; init; }
    public HaMediaPlayerItem[]? Children { get; init; }
    public double? NotShown { get; init; }
}
