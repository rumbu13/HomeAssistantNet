using HomeAssistantNet.Client;
using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaEntityState
{
    public string? EntityId { get; init; }
    public string? State { get; init; }
    public JsonElement? Attributes { get; init; }
    public DateTime? LastChanged { get; init; }
    public DateTime? LastUpdated { get; init; }
    public HaContext? Context { get; init; }
}
