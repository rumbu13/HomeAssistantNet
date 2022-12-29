using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaLovelaceDashboard
{
    public HaLovelaceDashboardMode? Mode { get; init; }
    public string? Id { get; init; }
    public string? UrlPath { get; init; }
    public bool? RequireAdmin { get; init; }
    public bool? ShowInSidebar { get; init; }
    public string? Icon { get; init; }
    public string? Title { get; init; }
    public string? Filename { get; init; }
}