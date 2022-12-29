namespace HomeAssistantNet.Api;

public sealed record HaServiceRemovedEvent : HaStandardEvent
{
    public HaServiceData? Data { get; init; }
}
