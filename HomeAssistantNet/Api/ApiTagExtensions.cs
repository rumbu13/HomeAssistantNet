using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiTagExtensions
{

    public static Task<HaTag[]?> GetTagsAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
       => client.GetListAsync<HaTag>("tag/list", cancellationToken);

    public static Task<HaTag?> CreateTagAsync(this IHaClient client, string? tagId = default,  string? name = default, 
        string? description = default, DateTime? lastScanned = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaTag>(new
        {
            type = "tag/create",
            tagId,
            name,
            description,
            lastScanned,
        }, cancellationToken);

    public static Task<HaTag?> UpdateTagAsync(this IHaClient client, string tagId, string? name = default,
        string? description = default, DateTime? lastScanned = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaTag>(new
        {
            type = "tag/update",
            tagId,
            name,
            description,
            lastScanned
        }, cancellationToken);

    public static Task<HaTag?> DeleteTagAsync(this IHaClient client, string tagId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaTag>(new
        {
            type = "tag/delete",
            tagId
        }, cancellationToken);

}