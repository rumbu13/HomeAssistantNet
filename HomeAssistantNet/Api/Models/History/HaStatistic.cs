using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaStatistic
{
    public string? StatisticId { get; init; }
    public bool? HasMean { get; init; }
    public bool? HasSum { get; init; }
    public string? Name { get; init; }
    public string? Source { get; init; }
    public string? UnitOfMeasurement { get; init; }
}