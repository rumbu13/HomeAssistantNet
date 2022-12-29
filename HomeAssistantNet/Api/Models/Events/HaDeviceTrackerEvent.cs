namespace HomeAssistantNet.Api;

public sealed record HaDeviceTrackerEvent : HaStandardEvent
{
    public HaDeviceTrackerData? Data { get; init; }
}
