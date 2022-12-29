using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiServiceExtensions
{
    public static Task<IDictionary<string, IDictionary<string, HaService>>?> GetServicesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<IDictionary<string, IDictionary<string, HaService>>>(
            new { type = "get_services" }, cancellationToken);    
    

    public static Task<HaEventContext?> CallServiceAsync(this IHaClient client, string domain, string service,
        IEnumerable<string>? targets = default, object? serviceData = default, 
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaEventContext>(new
        {
            type = "call_service",
            domain,
            service,
            targets = targets?.ToArray(),
            serviceData
        }, cancellationToken);

    public static Task<HaEventContext?> CallServiceAsync(this IHaClient client, string domain, string service,
        string target, object? serviceData = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaEventContext>(new
        {
            type = "call_service",
            domain,
            service,
            target = new string[] { target },
            serviceData
        }, cancellationToken);


}