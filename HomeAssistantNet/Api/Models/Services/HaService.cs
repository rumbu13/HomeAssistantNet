using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaService
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public IDictionary<string, HaField>? Fields { get; init; }
    public HaTargetSelector? Target { get; init; }
}