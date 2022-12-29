namespace HomeAssistantNet.Api;

public sealed record HaPlateDetectedData
{
    public double? Confidence { get; init; }
    public string? EntityId { get; init; }
    public string? Plate { get; init; }
}
