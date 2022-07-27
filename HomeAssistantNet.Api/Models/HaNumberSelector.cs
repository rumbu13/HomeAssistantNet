namespace HomeAssistantNet.Api;

public sealed record HaNumberSelector
{
    public double? Min { get; init; }
    public double? Max { get; init; }
    public double? Step { get; init; }
    public string? UnitOfMeasurement { get; init; }
    public string? Mode { get; init; }
}