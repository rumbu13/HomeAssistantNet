using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaDomainServices
{
    public string? Domain { get; init; }
    public IReadOnlyDictionary<string, HaService>? Services { get; init; }
}