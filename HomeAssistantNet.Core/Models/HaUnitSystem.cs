namespace HomeAssistantNet.Core;

public sealed record HaUnitSystem
{
    public string? Length { get; init; }
    public string? Mass { get; init; }
    public string? Temperature { get; init; }
    public string? Volume { get; init; }
    public string? AccumulatedPrecipitation { get; init; }
    public string? Pressure { get; init; }
    public string? WindSpeed { get; init; }

}