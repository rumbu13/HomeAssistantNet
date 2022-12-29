using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaStatisticValue
{
    public string? StatisticId { get; init; }
    public DateTime? Start { get; init; }
    public DateTime? End { get; init; }
    public string? LastReset { get; init; }
    public double? Max { get; init; }
    public double? Mean { get; init; }
    public double? Min { get; init; }
    public double? Sum { get; init; }
    public double? State { get; init; }
}
