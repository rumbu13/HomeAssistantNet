namespace HomeAssistantNet.Api;

public sealed record HaDeviceData
{
    public string? Action { get; init; }
    public string? DeviceId { get; init; }
    public HaDevice? Changes { get; init; }
}
