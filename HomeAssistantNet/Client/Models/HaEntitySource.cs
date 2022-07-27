using System.Text.Json;

namespace HomeAssistantNet.Client;

public sealed record HaEntitySource
{
    public string? Domain { get; init; }
    public bool? CustomComponent { get; init; }
    public string? Source { get; init; }
    public string? ConfigEntry { get; init; }
}