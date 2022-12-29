using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiMobileExtensions
{

    public static Task PushNotificationConfirmAsync(this IHaClient client, string webhookId, string confirmId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new 
        { 
            type = "mobile_app/push_notification_confirm", 
            webhookId,
            confirmId
        }, cancellationToken);

    public static Task PushNotificationChannelAsync(this IHaClient client, string webhookId, 
        bool? support_confirm = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "mobile_app/push_notification_channel",
            webhookId,
            support_confirm
        }, cancellationToken);


}