namespace HomeAssistantNet.Api;

public sealed record HaImage
{
    public string? Id { get; init; }
    public string? Name { get; init; }
    public double? Filesize { get; init; }
    public DateTime? UploadedAt { get; init; }
    public string? ContentType { get; init; }
}
