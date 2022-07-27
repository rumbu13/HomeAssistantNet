using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiServiceExtensions
{
    public static async Task<HaService[]?> GetServicesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
    {
        var services = await client.
            SendAsync<HaCommand, IDictionary<string, IDictionary<string, HaService>>>(
            new HaCommand("get_services"), cancellationToken);
        
        var result =  services?.SelectMany(ds => ds.Value.Select(hs => new HaService()
        {
            Description = hs.Value.Description,
            Domain = ds.Key,
            ServiceId = hs.Key,
            Fields = hs.Value.Fields,
            Name = hs.Value.Name,
            Target = hs.Value.Target
        }));            
        return result?.ToArray();                
    }

    public static Task<HaStateContext?> CallServiceAsync(this IHaClient client, string domain, string service,
        IEnumerable<string>? targets = default, object? serviceData = default, 
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaServiceCall, HaStateContext>(new HaServiceCall(domain, service, targets, serviceData), 
            cancellationToken);

    public static Task<HaStateContext?> CallServiceAsync(this IHaClient client, string domain, string service,
        string target, object? serviceData = default,
        CancellationToken cancellationToken = default)
        => client.SendAsync<HaServiceCall, HaStateContext>(new HaServiceCall(domain, service, new string[] { target }, 
            serviceData), cancellationToken);


}