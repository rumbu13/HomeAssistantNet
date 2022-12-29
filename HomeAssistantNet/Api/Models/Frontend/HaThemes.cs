using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaThemes
{
    public IDictionary<string, JsonElement>? Themes { get; init; }
    public string? DefaultTheme { get; init; }
    public string? DefaultDarkTheme { get; init; }
}