namespace HomeAssistantNet.Api;

public sealed record HaTagData
{
    public string? TagId { get; init; }
    public string? DeviceId { get; init; }
}
