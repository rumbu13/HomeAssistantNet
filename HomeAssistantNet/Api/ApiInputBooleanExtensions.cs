using HomeAssistantNet.Client;

namespace HomeAssistantNet.Api;

public static class ApiInputBooleanExtensions
{
    public static Task<HaInputBoolean[]?> GetInputBooleansAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
       => client.GetListAsync<HaInputBoolean>("input_boolean/list", cancellationToken);

    public static Task<HaInputBoolean?> CreateInputBooleanAsync(this IHaClient client, string name, 
        string? icon = default, bool? initial = default, CancellationToken cancellationToken = default)
       => client.SendCommandAsync<HaInputBoolean>(new
       {
           type = "input_boolean/create",
           name,
           icon,
           initial,
       }, cancellationToken);

    public static Task<HaInputBoolean?> UpdateInputBooleanAsync(this IHaClient client, string inputBooleanId, 
        string? name = default, string? icon = default, bool? initial = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaInputBoolean>(new
        {
            type = "input_boolean/update",
            inputBooleanId,
            name,
            icon,
            initial
        }, cancellationToken);

    public static Task DeleteInputBooleanAsync(this IHaClient client, string inputBooleanId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "input_boolean/delete",
            inputBooleanId
        }, cancellationToken);
}