using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiTimerExtensions
{

    public static Task<HaTimer[]?> GetTimersAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
       => client.GetListAsync<HaTimer>("timer/list", cancellationToken);

    public static Task<HaTimer?> CreateTimerAsync(this IHaClient client, string name, 
        string? icon = default, TimeSpan? duration = default, bool? restore = default, 
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaTimer>(new
        {
            type = "timer/create",
            name,
            icon,
            duration,
            restore,
        }, cancellationToken);

    public static Task<HaTimer?> UpdateTimerAsync(this IHaClient client, string timerId, string? name = default,
         string? icon = default, TimeSpan? duration = default, bool? restore = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaTimer>(new
        {
            type = "timer/update",
            timerId,
            name,
            icon,
            duration,
            restore
        }, cancellationToken);

    public static Task<HaTimer?> DeleteTimerAsync(this IHaClient client, string timerId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaTimer>(new
        {
            type = "timer/delete",
            timerId
        }, cancellationToken);

}