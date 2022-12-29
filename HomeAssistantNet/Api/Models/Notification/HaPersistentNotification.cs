namespace HomeAssistantNet.Api;

public sealed record HaPersistentNotification
{
    public string? NotificationId { get; init; }
    public string? Message { get; init; }
    public HaNotificationStatus? Status { get; init; }
    public string? Title { get; init; }
    public DateTime? CreatedAt { get; init; }
}
