using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaNumberSelector
{
    public double? Min { get; init; }
    public double? Max { get; init; }
    [JsonConverter(typeof(JsonAlwaysStringConverter))]
    public string? Step { get; init; }
    public string? UnitOfMeasurement { get; init; }
    public HaNumberMode? Mode { get; init; }
}