namespace HomeAssistantNet.Api;

public sealed record HaDevicesUpdatedData
{
    public string? Action { get; init; }
    public string? DeviceId { get; init; }
    public HaDevice? Changes { get; init; }
}
