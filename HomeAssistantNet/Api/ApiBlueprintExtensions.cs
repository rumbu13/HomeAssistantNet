using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiBlueprintExtensions
{
    public static Task<IDictionary<string, HaBlueprint>?> GetBlueprintsAsync(this IHaClient client, string domain,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<IDictionary<string, HaBlueprint>>(
            new { type = "blueprint/list", domain }, cancellationToken);
        

    public static Task<HaBlueprintImport?> ImportBlueprintAsync(this IHaClient client, string url, 
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaBlueprintImport>(new { type = "blueprint/import", url }, 
            cancellationToken);

    public static Task SaveBlueprintAsync(this IHaClient client, string domain, string path, string yaml, 
        string? sourceUrl = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<Object>(new { type = "blueprint/save", domain, path, yaml, sourceUrl }, 
            cancellationToken);

    public static Task DeleteBlueprintAsync(this IHaClient client, string domain, string path, 
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<Object>(new { type = "blueprint/delete", domain, path },  cancellationToken);

}