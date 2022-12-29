using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiNotificationExtensions
{

    public static Task<HaPersistentNotification[]?> GetPersistentNotificationsAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.GetListAsync<HaPersistentNotification>("persistent_notification/get", cancellationToken);




}