namespace HomeAssistantNet.Api;

public sealed record HaTagTrigger : HaTrigger
{
    public string? TagId { get; init; }
    public string? DeviceId { get; init; }
}
