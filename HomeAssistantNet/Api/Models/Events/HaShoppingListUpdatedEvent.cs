namespace HomeAssistantNet.Api;

public sealed record HaShoppingListUpdatedEvent : HaStandardEvent
{
    public HaShoppingListUpdatedData? Data { get; init; }
}
