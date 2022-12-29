namespace HomeAssistantNet.Api;

public sealed record HaUserAddedEvent : HaStandardEvent
{
    public HaUserData? Data { get; init; }
}
