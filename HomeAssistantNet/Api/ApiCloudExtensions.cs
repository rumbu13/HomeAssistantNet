using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiCloudExtensions
{
  
    public static Task<HaCloudStatus?> GetCloudStatusAsync(this IHaClient client, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaCloudStatus>(new
        {
            type = "cloud/status"
        }, cancellationToken);

    public static Task<HaSubscriptionInfo?> GetCloudSubscriptionAsync(this IHaClient client, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaSubscriptionInfo>(new
        {
            type = "cloud/subscription"
        }, cancellationToken);

    public static Task UpdateCloudPreferencesAsync(this IHaClient client,
        bool? enableGoogle = default, bool? enableAlexa = default, bool? alexaReportState = default,
        bool? googleReportState = default, IEnumerable<string>? alexaDefaultExpose = default, 
        IEnumerable<string>? googleDefaultExpose = default, string? googleSecureDevicesPin = default, 
        IEnumerable<IEnumerable<string>>? ttsDefaultVoice = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<Object>(new
        {
            type = "cloud/update_prefs",
            enableGoogle,
            enableAlexa,
            alexaReportState,
            googleReportState,
            alexaDefaultExpose = alexaDefaultExpose?.ToArray(),
            googleDefaultExpose = googleDefaultExpose?.ToArray(),
            googleSecureDevicesPin,
            ttsDefaultVoice = ttsDefaultVoice?.Select(v => v.Select(u => u.ToArray())).ToArray(),

        }, cancellationToken);

    public static Task<HaCloudwebhook?> CreateCloudhookAsync(this IHaClient client, string webhookId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaCloudwebhook>(new
        {
            type = "cloud/cloudhook/create",
            webhookId,
        }, cancellationToken);

    public static Task DeleteCloudhookAsync(this IHaClient client, string webhookId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "cloud/cloudhook/delete",
            webhookId,
        }, cancellationToken);

    public static Task<HaCloudStatus?> ConnectCloudRemoteAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaCloudStatus>(new
        {
            type = "cloud/remote/connect"
        }, cancellationToken);

    public static Task<HaCloudStatus?> DisconnectCloudRemoteAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaCloudStatus>(new
        {
            type = "cloud/remote/disconnect"
        }, cancellationToken);

    public static Task<HaGoogleEntity[]?> GetCloudGoogleEntitiesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaGoogleEntity[]>(new
        {
            type = "cloud/google_assistant/entities"
        }, cancellationToken);

    public static Task<HaGoogleEntityConfig?> UpdateCloudGoogleEntityAsync(this IHaClient client, string entityId, 
        bool? shouldExpose = default, string? overrideName = default, IEnumerable<string>? aliases = default, 
        bool? disable_2fa = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaGoogleEntityConfig>(new
        {
            type = "cloud/google_assistant/entities/update",
            shouldExpose,
            overrideName,
            aliases = aliases?.ToArray(),
            disable_2fa
        }, cancellationToken);

    public static Task<HaAlexaEntity[]?> GetCloudAlexaEntitiesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.GetListAsync<HaAlexaEntity>("cloud/alexa/entities", cancellationToken);

    public static Task SyncCloudAlexaEntitiesAsync(this IHaClient client, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<Object>(new
        {
            type = "cloud/alexa/sync"
        }, cancellationToken);

    public static Task<HaAlexaEntityConfig?> UpdateCloudAlexaEntityAsync(this IHaClient client, string entityId,
        bool? shouldExpose = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaAlexaEntityConfig>(new
        {
            type = "cloud/alexa/entities/update",
            shouldExpose
        }, cancellationToken);

    public static Task<HaThingTalkConversion?> ConvertCloudThingTalkAsync(this IHaClient client, string query, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaThingTalkConversion>(new
        {
            type = "cloud/thingtalk/convert",
            query
        }, cancellationToken);

    public static Task<HaTtsInfo?> GetCloudTtsInfoAsync(this IHaClient client, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaTtsInfo>(new
        {
            type = "cloud/tts/info"
        }, cancellationToken);

    public static Task<HaAlexaEntity[]?> GetAlexaEntitiesAsync(this IHaClient client,
    CancellationToken cancellationToken = default)
    => client.GetListAsync<HaAlexaEntity>("cloud/alexa/entities", cancellationToken);

    public static Task SyncAlexaEntitiesAsync(this IHaClient client, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<Object>(new
        {
            type = "cloud/alexa/sync"
        }, cancellationToken);
}