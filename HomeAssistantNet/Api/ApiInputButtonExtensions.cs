using HomeAssistantNet.Client;

namespace HomeAssistantNet.Api;

public static class ApiInputButtonExtensions
{
    public static Task<HaInputButton[]?> GetInputButtonsAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
       => client.GetListAsync<HaInputButton>("input_button/list", cancellationToken);

    public static Task<HaInputButton?> CreateInputButtonAsync(this IHaClient client, string name, 
        string? icon = default, CancellationToken cancellationToken = default)
       => client.SendCommandAsync<HaInputButton>(new
       {
           type = "input_button/create",
           name,
           icon,
       }, cancellationToken);

    public static Task<HaInputButton?> UpdateInputButtonAsync(this IHaClient client, string inputButtonId, 
        string? name = default, string? icon = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaInputButton>(new
        {
            type = "input_button/update",
            inputButtonId,
            name,
            icon,
        }, cancellationToken);

    public static Task DeleteInputButtonAsync(this IHaClient client, string inputButtonId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "input_button/delete",
            inputButtonId
        }, cancellationToken);
}