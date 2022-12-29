using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiMediaExtensions
{
    
    public static Task<HaMediaPlayerItem?> BrowseMediaPlayerAsync(this IHaClient client, string entityId, 
        string? mediaContentId = default, string? mediaContentType = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaMediaPlayerItem>(new
        {
            type = "media_player/browse_media",
            entityId,
            mediaContentId,
            mediaContentType
        }, cancellationToken);

    public static Task<HaMediaPlayerItem?> BrowseMediaSourceAsync(this IHaClient client, string? mediaContentId = default,
       CancellationToken cancellationToken = default)
       => client.SendCommandAsync<HaMediaPlayerItem>(new
       {
           type = "media_source/browse_media",
           mediaContentId
       }, cancellationToken);

    public static Task<HaMediaSourceInfo?> ResolveMediaSourceAsync(this IHaClient client, string mediaContentId,
       int? expires = default, CancellationToken cancellationToken = default)
       => client.SendCommandAsync<HaMediaSourceInfo>(new
       {
           type = "media_source/resolve_media",
           mediaContentId,
           expires
       }, cancellationToken);

    public static Task DeleteMediaSourceAsync(this IHaClient client, string mediaContentId,
       CancellationToken cancellationToken = default)
       => client.SendCommandAsync<object>(new
       {
           type = "media_source/local_source/remove",
           mediaContentId           
       }, cancellationToken);
}