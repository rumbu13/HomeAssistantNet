namespace HomeAssistantNet.Api;

public sealed record HaUserRemovedEvent : HaStandardEvent
{
    public HaUserData? Data { get; init; }
}
