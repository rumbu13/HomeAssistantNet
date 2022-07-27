namespace HomeAssistantNet.Client.Internal;

internal sealed class HaEntityGet : HaCommand
{
    public HaEntityGet(string entityId)
        : base("config/entity_registry/get")
    {
        EntityId = entityId;
    }
    public string EntityId { get; init; }
}

internal sealed class HaEntityGetSource : HaCommand
{
    public HaEntityGetSource(IEnumerable<string>? entityIds)
        : base("entity/source")
    {
        EntityId = entityIds?.ToArray();
    }
    public string[]? EntityId { get; init; }
}


