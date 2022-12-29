using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiApplicationCredentialExtensions
{
  
    public static Task<HaApplicationCredential[]?> GetApplicationCredentialsAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.GetListAsync<HaApplicationCredential>("application_credentials/list", cancellationToken);

    public static Task<HaApplicationCredentialsConfig?> GetApplicationCredentialsConfigAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaApplicationCredentialsConfig>(new 
        { 
            type = "application_credentials/config" 
        }, cancellationToken);

    public static Task<HaApplicationCredential?> CreateApplicationCredentialAsync(this IHaClient client, string domain, 
        string clientId, string clientSecret, string? authDomain = default, string? name = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaApplicationCredential>(new
        {
            type = "application_credentials/create",
            domain,
            clientId,
            clientSecret,
            authDomain,
            name
        }, cancellationToken);

    public static Task DeleteApplicationCredentialAsync(this IHaClient client, string applicationCredentialsId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<Object>(new
        {
            type = "application_credentials/delete",
            applicationCredentialsId
        }, cancellationToken);



}