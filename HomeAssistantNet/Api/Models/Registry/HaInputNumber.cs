namespace HomeAssistantNet.Api;

public record HaInputNumber
{
    public string? Id { get; init; }
    public string? Icon { get; init; }
    public double? Initial { get; init; }
    public string? Name { get; init; }
    public double? Max { get; init; }
    public double? Min { get; init; }
    public double? Step { get; init; }
    public string? UnitOfMeasurement { get; init; }
    public HaNumberMode? Mode { get; init; }
}
