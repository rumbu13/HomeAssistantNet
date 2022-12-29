namespace HomeAssistantNet.Api;

public sealed record HaTag
{
    public string? Id { get; init; }
    public string? TagId { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public DateTime? LastScanned { get; init; }
}
