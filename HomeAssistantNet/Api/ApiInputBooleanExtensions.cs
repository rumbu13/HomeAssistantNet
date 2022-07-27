using HomeAssistantNet.Client;

namespace HomeAssistantNet.Api;

public static class ApiInputBooleanExtensions
{
    public static Task<HaInputBoolean[]?> GetInputBooleansAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
       => client.GetListAsync<HaInputBoolean>("input_boolean/list", cancellationToken);
}