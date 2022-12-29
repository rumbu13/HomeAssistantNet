namespace HomeAssistantNet.Api;

public sealed record HaWebhookTrigger : HaTrigger
{
    public string? WebhookId { get; init; }
}
