namespace HomeAssistantNet.Api;

public sealed record HaGoalData
{
    public string? Team { get; init; }
    public string? TeamName { get; init; }
    public string? TeamHash { get; init; }
    public int? LeagueId { get; init; }
    public string? LeagueName { get; init; }
}
