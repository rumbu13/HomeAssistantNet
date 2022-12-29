using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaFrontendVersion
{
    public string? Version { get; init; }
}