using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiFrontendExtensions
{
    public static Task<HaTranslation?> GetFrontendTranslationsAsync(this IHaClient client, string language,
        HaTranslationCategory category, IEnumerable<string>? integration = default, bool? configFlow = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaTranslation>(new
        {
            type = "frontend/get_translations",
            language,
            category,
            integration = integration?.ToArray(),
            configFlow,
        }, cancellationToken);

    public static Task<HaFrontendVersion?> GetFrontendVersionAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaFrontendVersion>(new
        {
            type = "frontend/get_version"
        }, cancellationToken);

    public static Task SetFrontendUserDataAsync(this IHaClient client, string key, object value,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaFrontendVersion>(new
        {
            type = "frontend/set_user_data",
            key,
            value
        }, cancellationToken);

    public static Task<object?> GetFrontendUserDataAsync(this IHaClient client, string key,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "frontend/get_user_data",
            key
        }, cancellationToken);

    public static Task<IDictionary<string, HaPanel>?> GetFrontendPanelsAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<IDictionary<string, HaPanel>>(new
        {
            type = "get_panels"
        }, cancellationToken);

    public static Task<HaThemes?> GetFrontendThemesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaThemes>(new
        {
            type = "frontend/get_themes"
        }, cancellationToken);

    public static Task<HaLovelaceResource[]?> GetLovelaceResourcesAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.GetListAsync<HaLovelaceResource>("lovelace/resources", cancellationToken);

    public static Task<HaLovelaceResource?> CreateLovelaceResource(this IHaClient client, HaLovelaceResourceType resType, 
        string url, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaLovelaceResource>(new
        {
            type = "lovelace/resources/create",
            resType,
            url,
        }, cancellationToken);

    public static Task<HaLovelaceResource?> UpdateLovelaceResourceAsync(this IHaClient client, string resourceId, 
        HaLovelaceResourceType? resType = default, string? url = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaLovelaceResource>(new
        {
            type = "lovelace/resources/update",
            resourceId,
            resType,
            url,
        }, cancellationToken);

    public static Task DeleteLovelaceResourceAsync(this IHaClient client, string resourceId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "lovelace/resources/delete",
            resourceId,
        }, cancellationToken);

    public static Task<HaLovelaceConfig?> GetLovelaceConfigAsync(this IHaClient client, bool force, 
        string? urlPath = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaLovelaceConfig>(new
        {
            type = "lovelace/config",
            urlPath,
            force
        }, cancellationToken);

    public static Task SaveLovelaceConfigAsync(this IHaClient client, HaLovelaceConfig config, string? urlPath = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "lovelace/config/save",
            urlPath,
            config
        }, cancellationToken);

    public static Task DeleteLovelaceConfigAsync(this IHaClient client, string? urlPath = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "lovelace/config/delete",
            urlPath
        }, cancellationToken);

    public static Task<HaLovelaceDashboard[]?> GetLovelaceDashboardsAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.GetListAsync<HaLovelaceDashboard>("lovelace/dashboards/list", cancellationToken);

    public static Task<HaLovelaceDashboard?> CreateLovelaceDashboardAsync(this IHaClient client, string urlPath, bool requireAdmin, 
        bool showInSidebar, string title, string? icon = default, CancellationToken cancellationToken = default)
       => client.SendCommandAsync<HaLovelaceDashboard>(new
       {
           type = "lovelace/dashboards/create",
           mode = "storage",
           urlPath,
           requireAdmin,
           showInSidebar,
           title,
           icon,
       }, cancellationToken);

    public static Task<HaLovelaceDashboard?> UpdateLovelaceDashboardAsync(this IHaClient client, string dashboardId, 
        string? urlPath = default, bool? requireAdmin = default, bool? showInSidebar = default, string? title = default,
        string? icon = default, CancellationToken cancellationToken = default)
       => client.SendCommandAsync<HaLovelaceDashboard>(new
       {
           type = "lovelace/dashboards/update",
           dashboardId,
           urlPath,
           requireAdmin,
           showInSidebar,
           title,
           icon,
       }, cancellationToken);

    public static Task DeleteLovelaceDashboardAsync(this IHaClient client, string dashboardId,
        CancellationToken cancellationToken = default)
       => client.SendCommandAsync<object>(new
       {
           type = "lovelace/dashboards/delete",
           dashboardId
       }, cancellationToken);


}