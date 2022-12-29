using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiCameraExtensions
{

    public static Task<HaCameraWebRtc?> GetCameraWebRtcAsync(this IHaClient client, string entityId, string offer,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaCameraWebRtc>(new
        {
            type = "camera/web_rtc_offer",
            entityId,
            offer
        }, cancellationToken);

    public static Task<HaCameraStream?> GetCameraStreamAsync(this IHaClient client, string entityId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaCameraStream>(new 
        { 
            type = "camera/stream",
            entityId
        }, cancellationToken);

    public static Task<HaCameraPrefs?> GetCameraPreferencesAsync(this IHaClient client, string entityId, 
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaCameraPrefs>(new
        {
            type = "camera/get_prefs",
            entityId
        }, cancellationToken);

    public static Task<HaCameraPrefs?> UpdateCameraPreferencesAsync(this IHaClient client, string entityId, 
        bool preloadStream, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaCameraPrefs>(new
        {
            type = "camera/update_prefs",
            entityId,
            preloadStream
        }, cancellationToken);



}