using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaDeconzData
{
    public string? Id { get; init; }
    public string? UniqueId { get; init; }
    public int? Gesture { get; init; }
    public int? Angle { get; init; }
    public (double, double) XY { get; init; }
    public string? Event { get; init; }
}
