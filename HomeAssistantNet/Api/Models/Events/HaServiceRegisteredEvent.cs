namespace HomeAssistantNet.Api;

public sealed record HaServiceRegisteredEvent : HaStandardEvent
{
    public HaServiceData? Data { get; init; }
}
