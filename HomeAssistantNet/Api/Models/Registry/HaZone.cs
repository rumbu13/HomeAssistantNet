namespace HomeAssistantNet.Api;

public sealed record HaZone
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public string? Icon { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public double? Radius { get; init; }
    public bool? Passive { get; init; }

}
