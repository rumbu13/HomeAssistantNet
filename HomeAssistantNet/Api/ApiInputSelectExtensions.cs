using HomeAssistantNet.Client;

namespace HomeAssistantNet.Api;

public static class ApiInputSelectExtensions
{
    public static Task<HaInputSelect[]?> GetInputSelectsAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
       => client.GetListAsync<HaInputSelect>("input_select/list", cancellationToken);

    public static Task<HaInputSelect?> CreateInputSelectAsync(this IHaClient client, string name, 
        IEnumerable<string> options, string? icon = default, bool? initial = default,
        CancellationToken cancellationToken = default)
       => client.SendCommandAsync<HaInputSelect>(new
       {
           type = "input_select/create",
           name,
           icon,
           initial,
           options = options.ToArray()
       }, cancellationToken);

    public static Task<HaInputSelect?> UpdateInputSelectAsync(this IHaClient client, string inputSelectId, 
        string? name = default, IEnumerable<string>? options = default, string? icon = default, bool? initial = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaInputSelect>(new
        {
            type = "input_select/update",
            inputSelectId,
            name,
            icon,
            initial,
            options = options?.ToArray()
        }, cancellationToken);

    public static Task DeleteInputSelectAsync(this IHaClient client, string inputSelectId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "input_select/delete",
            inputSelectId
        }, cancellationToken);
}