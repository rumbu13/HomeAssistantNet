namespace HomeAssistantNet.Api;

public sealed record HaAlexaEntity
{
    public string? EntityId { get; init; }
    public string[]? DisplayCategories { get; set; }
    public string[]? Interfaces { get; set; }
}
