namespace HomeAssistantNet.Api;

public sealed record HaUserUpdatedEvent : HaStandardEvent
{
    public HaUserData? Data { get; init; }
}
