namespace HomeAssistantNet.Api;

public sealed record HaDeviceTrackerData
{
    public string? EntityId { get; init; }
    public string? HostName { get; init; }
    public string? Mac { get; init; }
}