namespace HomeAssistantNet.Api;

public sealed record HaShoppingData
{
    public string? Action { get; init; }
    public string? Item { get; init; }
}
