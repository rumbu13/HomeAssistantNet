namespace HomeAssistantNet.Api;

public sealed record HaSubscriptionInfo
{
    public string? HumanDescription { get; init; }
    public string? Provider { get; set; }
    public double? Managed { get; set; }
}
