namespace HomeAssistantNet.Api;

public sealed record HaRepairsUpdatedData
{
    public string? Action { get; init; }
    public string? Domain { get; init; }
    public string? IssueId { get; init; }
}
