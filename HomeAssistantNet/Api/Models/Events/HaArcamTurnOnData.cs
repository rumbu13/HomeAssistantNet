using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaArcamTurnOnData
{
    public string? EntityId { get; init; }
}
