namespace HomeAssistantNet.Api;

public sealed record HaShoppingListUpdatedData
{
    public string? Action { get; init; }
    public string? Item { get; init; }
}
