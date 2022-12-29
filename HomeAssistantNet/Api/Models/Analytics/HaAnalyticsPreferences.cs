namespace HomeAssistantNet.Api;

public sealed record HaAnalyticsPreferences
{
    public bool? Base { get; init; }
    public bool? Diagnostics { get; init; }
    public bool? Usage { get; init; }
    public bool? Statistics { get; init; }
}