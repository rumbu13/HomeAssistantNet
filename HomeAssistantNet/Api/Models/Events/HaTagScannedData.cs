namespace HomeAssistantNet.Api;

public sealed record HaTagScannedData
{
    public string? TagId { get; init; }
    public string? DeviceId { get; init; }
}
