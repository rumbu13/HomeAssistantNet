namespace HomeAssistantNet.Api;

public sealed record HaLovelaceUpdatedEvent : HaStandardEvent
{
    public HaLovelaceUpdatedData? Data { get; init; }
}
