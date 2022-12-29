using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiStateExtensions
{
    public static Task<HaEntityState[]?> GetStatesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.GetListAsync<HaEntityState>("get_states", cancellationToken);
}