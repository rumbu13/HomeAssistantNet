using HomeAssistantNet.Client;

namespace HomeAssistantNet.Api;

public static class ApiInputNumberExtensions
{
    public static Task<HaInputNumber[]?> GetInputNumbersAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
       => client.GetListAsync<HaInputNumber>("input_number/list", cancellationToken);

    public static Task<HaInputNumber?> CreateInputNumberAsync(this IHaClient client, string name, double min, double max,
        string? icon = default, double? initial = default, double? step = default, string? unitOfMeasurement = default,
        HaNumberMode? mode = default, CancellationToken cancellationToken = default)
       => client.SendCommandAsync<HaInputNumber>(new
       {
           type = "input_number/create",
           name,
           min,
           max,
           icon,
           initial,
           step,
           unitOfMeasurement,
           mode,
       }, cancellationToken);

    public static Task<HaInputNumber?> UpdateInputNumberAsync(this IHaClient client, string inputNumberId,
        string? name = default, double? min = default, double? max = default, string? icon = default, double? initial = default, 
        double? step = default, string? unitOfMeasurement = default,
        HaNumberMode? mode = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaInputNumber>(new
        {
            type = "input_number/update",
            inputNumberId,
            name,
            min,
            max,
            icon,
            initial,
            step,
            unitOfMeasurement,
            mode,
        }, cancellationToken);

    public static Task DeleteInputNumberAsync(this IHaClient client, string inputNumberId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "input_number/delete",
            inputNumberId
        }, cancellationToken);
}