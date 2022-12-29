using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiPersonExtensions
{

    public static Task<HaPersonList?> GetPersonsAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaPersonList>(new 
        { 
            type = "person/list"
        }, cancellationToken);


    public static Task<HaPerson?> CreatePersonAsync(this IHaClient client, string name,
        string? userId = default, IEnumerable<string>? deviceTrackers = default, string? picture = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaPerson>(new
        {
            type = "person/create",
            userId,
            deviceTrackers = deviceTrackers?.ToArray(),
            picture
        }, cancellationToken);

    public static Task<HaPerson?> UpdatePersonAsync(this IHaClient client, string personId, string? name = default,
        string? userId = default, IEnumerable<string>? deviceTrackers = default, string? picture = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaPerson>(new
        {            
            type = "person/update",
            personId,
            userId,
            deviceTrackers = deviceTrackers?.ToArray(),
            picture
        }, cancellationToken);

    public static Task DeletePersonAsync(this IHaClient client, string personId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "person/delete",
            personId
        }, cancellationToken);
}