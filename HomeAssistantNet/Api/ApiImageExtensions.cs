using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiImageExtensions
{

    public static Task<HaImage[]?> GetImagesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
       => client.GetListAsync<HaImage>("image/list", cancellationToken);

    public static Task<HaImage?> UpdateImageAsync(this IHaClient client, string imageId, string? name,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaImage>(new
        {
            type = "image/update",
            imageId,
            name
        }, cancellationToken);

    public static Task DeleteImageAsync(this IHaClient client, string imageId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "image/delete",
            imageId
        }, cancellationToken);

}