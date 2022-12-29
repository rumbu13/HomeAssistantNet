using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaEntitySource
{
    public string? Domain { get; init; }
    public bool? CustomComponent { get; init; }
    public string? Source { get; init; }
    public string? ConfigEntry { get; init; }
}