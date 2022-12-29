using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiCounterExtensions
{

    public static Task<HaCounter[]?> GetCountersAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
       => client.GetListAsync<HaCounter>("counter/list", cancellationToken);

    public static Task<HaCounter?> CreateCounterAsync(this IHaClient client, string name, string? icon = default,
        int? initial = default, int? minimum = default, int? maximum = default, bool? restore = default, 
        int? step = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaCounter>(new
        {
            type = "counter/create",
            name,
            icon,
            initial,
            minimum,
            maximum,
            restore,
            step,
        }, cancellationToken);

    public static Task<HaCounter?> UpdateCounterAsync(this IHaClient client, string counterId, string? name = default,
        string? icon = default, int? initial = default, int? minimum = default, int? maximum = default, 
        bool? restore = default, int? step = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaCounter>(new
        {
            type = "counter/update",
            counterId,
            name,
            icon,
            initial,
            minimum,
            maximum,
            restore,
            step,
        }, cancellationToken);

    public static Task<HaCounter?> DeleteCounterAsync(this IHaClient client, string counterId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaCounter>(new
        {
            type = "counter/delete",
            counterId
        }, cancellationToken);

}