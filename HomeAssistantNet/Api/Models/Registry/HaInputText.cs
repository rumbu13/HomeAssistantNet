namespace HomeAssistantNet.Api;

public sealed record HaInputText
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public int? Min { get; init; }
    public int? Max { get; init; }
    public string? Initial { get; init; }
    public string? Icon { get; init; }
    public string? UnitOfMeasurement { get; init; }
    public string? Pattern { get; init; }
    public HaTextMode? Mode { get; init; }
}
