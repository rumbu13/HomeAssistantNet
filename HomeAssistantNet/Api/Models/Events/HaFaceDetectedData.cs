namespace HomeAssistantNet.Api;

public sealed record HaFaceDetectedData
{
    public double? Confidence { get; init; }
    public string? Name { get; init; }
    public double? Age { get; init; }
    public string? Gender { get; init; }
    public string? Motion { get; init; }
    public string? Glasses { get; init; }
    public double? TotalFaces { get; init; }
}
