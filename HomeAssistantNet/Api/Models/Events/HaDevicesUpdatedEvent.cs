namespace HomeAssistantNet.Api;

public sealed record HaDevicesUpdatedEvent : HaStandardEvent
{
    public HaDevicesUpdatedData? Data { get; init; }
}
