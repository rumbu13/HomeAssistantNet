using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaApiService
{
    public string? Domain { get; init; }
    public IDictionary<string, HaService>? Services { get; init; }
}