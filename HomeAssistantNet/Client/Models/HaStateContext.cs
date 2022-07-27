namespace HomeAssistantNet.Client;

public sealed record HaStateContext
{
    public string? Id { get; init; }
    public string? ParentId { get; init; }
    public string? UserId { get; init; }
}