using HomeAssistantNet.Client;

namespace HomeAssistantNet.Api;

public static class ApiInputDateTimeExtensions
{
    public static Task<HaInputDateTime[]?> GetInputDateTimesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
       => client.GetListAsync<HaInputDateTime>("input_datetime/list", cancellationToken);

    public static Task<HaInputDateTime?> CreateInputDateTimeAsync(this IHaClient client, string name, 
        string? icon = default, DateTime? initial = default, bool? hasDate = default, bool? hasTime = default,
        CancellationToken cancellationToken = default)
       => client.SendCommandAsync<HaInputDateTime>(new
       {
           type = "input_datetime/create",
           name,
           icon,
           initial,
           hasDate,
           hasTime,
       }, cancellationToken);

    public static Task<HaInputDateTime?> UpdateInputDateTimeAsync(this IHaClient client, string inputDatetimeId, 
        string? name = default, string? icon = default, bool? initial = default, bool? hasDate = default, 
        bool? hasTime = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaInputDateTime>(new
        {
            type = "input_datetime/update",
            inputDatetimeId,
            name,
            icon,
            initial,
            hasDate,
            hasTime
        }, cancellationToken);

    public static Task DeleteInputDateTimeAsync(this IHaClient client, string inputDatetimeId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "input_datetime/delete",
            inputDatetimeId
        }, cancellationToken);
}