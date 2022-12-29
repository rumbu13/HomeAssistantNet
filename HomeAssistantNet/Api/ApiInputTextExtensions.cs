using HomeAssistantNet.Client;

namespace HomeAssistantNet.Api;

public static class ApiInputTextExtensions
{
    public static Task<HaInputText[]?> GetInputTextsAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
       => client.GetListAsync<HaInputText>("input_text/list", cancellationToken);

    public static Task<HaInputText?> CreateInputTextAsync(this IHaClient client, string name, 
        int? min = default, int? max = default, string? icon = default, bool? initial = default, 
        string? unitOfMeasurement = default, string? pattern = default, HaTextMode? mode = default,
        CancellationToken cancellationToken = default)
       => client.SendCommandAsync<HaInputText>(new
       {
           type = "input_text/create",
           name,
           icon,
           initial,
           unitOfMeasurement,
           pattern,
           mode,
           min,
           max
       }, cancellationToken);

    public static Task<HaInputText?> UpdateInputTextAsync(this IHaClient client, string inputTextId,
        string? name = default, int? min = default, int? max = default, string? icon = default, bool? initial = default,
        string? unitOfMeasurement = default, string? pattern = default, HaTextMode? mode = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaInputText>(new
        {
            type = "input_text/update",
            name,
            icon,
            initial,
            unitOfMeasurement,
            pattern,
            mode,
            min,
            max
        }, cancellationToken);

    public static Task DeleteInputTextAsync(this IHaClient client, string inputTextId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "input_text/delete",
            inputTextId
        }, cancellationToken);
}