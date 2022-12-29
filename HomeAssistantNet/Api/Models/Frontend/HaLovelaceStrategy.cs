using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaLovelaceStrategy
{
    public string? Type { get; init; }
    public IDictionary<string, JsonElement>? Options { get; init; }

}