using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiCounterExtensions
{

    public static Task<HaCounter[]?> GetCountersAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
       => client.GetListAsync<HaCounter>("counter/list", cancellationToken);

}