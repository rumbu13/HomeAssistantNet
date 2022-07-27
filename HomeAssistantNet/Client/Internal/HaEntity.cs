using HomeAssistantNet.Client;

namespace HomeAssistantNet.Client.Internal;

internal sealed record HaEntityGet : HaWsCommand
{
    public HaEntityGet(string entityId)
        : base("config/entity_registry/get")
    {
        EntityId = entityId;
    }
    public string EntityId { get; init; }
}


