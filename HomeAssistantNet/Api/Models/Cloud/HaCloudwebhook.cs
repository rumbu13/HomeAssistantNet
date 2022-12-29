namespace HomeAssistantNet.Api;

public sealed record HaCloudwebhook
{
    public string? WebhookId { get; init; }
    public string? CloudhookId { get; init; }
    public string? CloudhookUrl { get; init; }
    public bool? Managed { get; init; }
}
