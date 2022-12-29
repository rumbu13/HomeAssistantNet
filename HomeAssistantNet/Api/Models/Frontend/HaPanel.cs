using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaPanel
{
    public string? ComponentName { get; init; }
    public string? Icon { get; init; }
    public string? Title { get; init; }
    public string? UrlPath { get; init; }
    public IDictionary<string, JsonElement?>? Config { get; init; }
}