using HomeAssistantNet.Client;
using System.Collections.Specialized;
using System.Text;

namespace HomeAssistantNet.Api;

public static class RestExtensions
{


    public static Task<HaApiState?> GetApiState(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.GetAsync<HaApiState>("/api", cancellationToken);

    public static Task<HaConfig?> GetConfigAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.GetAsync<HaConfig?>("/api/config", cancellationToken);

    public static Task<IReadOnlyList<HaApiEvent>?> GetEventsAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.GetAsync<IReadOnlyList<HaApiEvent>>("/api/events", cancellationToken);

    public static Task<IReadOnlyList<HaEntityState>?> GetStatesAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.GetAsync<IReadOnlyList<HaEntityState>>("/api/states", cancellationToken);

    public static Task<IReadOnlyList<HaDomainServices>?> GetServicesAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.GetAsync<IReadOnlyList<HaDomainServices>>("/api/services", cancellationToken);

    public static Task<HaEntityState?> GetStateAsync(this IHaRestClient client, string entityId,
        CancellationToken cancellationToken = default)
        => client.GetAsync<HaEntityState>($"/api/states/{entityId}", cancellationToken);

    public static Task<IReadOnlyList<HaHistory>?> GetHistory(
        this IHaRestClient client, DateTime? startTime, IEnumerable<string>? entityIds = default,
        DateTime? endTime = default, bool? minimalResponse = default, bool? noAttributes = default,
        bool? significantChangesOnly = default, CancellationToken cancellationToken = default)
    {
        StringBuilder sb = new("/api/history/period/");
        if (startTime is not null)
            sb.Append(startTime.Value.ToString("o"));

        NameValueCollection query = new();
        if (entityIds is not null)
            query.Add("filter_entity_id", String.Join(',', entityIds));
        if (endTime is not null)
            query.Add("end_time", endTime.Value.ToString("o"));
        if (minimalResponse is not null && minimalResponse.Value)
            query.Add("minimal_response", null);
        if (noAttributes is not null && noAttributes.Value)
            query.Add("no_attributes", null);
        if (significantChangesOnly is not null && significantChangesOnly.Value)
            query.Add("significant_changes_only", null);

        string? s = query.ToString();

        if (!string.IsNullOrEmpty(s))
        {
            sb.Append('_');
            sb.Append(s);
        }

        return client.GetAsync<IReadOnlyList<HaHistory>>(sb.ToString(), cancellationToken);

    }


    public static Task<IReadOnlyList<HaApiLogbookEntry>?> GetLogbookEntriesAsync(this IHaRestClient client, string? entityId = default,
        DateTime? startTime = default, DateTime? endTime = default, CancellationToken cancellationToken = default)
    {
        StringBuilder sb = new("/api/logbook/");
        if (startTime is not null)
            sb.Append(startTime.Value.ToString("o"));

        NameValueCollection query = new();
        if (entityId is not null)
            query.Add("entity", entityId);
        if (endTime is not null)
            query.Add("end_time", endTime.Value.ToString("o"));

        string? s = query.ToString();

        if (!string.IsNullOrEmpty(s))
        {
            sb.Append('?');
            sb.Append(s);
        }

        return client.GetAsync<IReadOnlyList<HaApiLogbookEntry>>(sb.ToString(), cancellationToken);

    }

    public static Task<string?> GetErrorLogAsync(this IHaRestClient client, CancellationToken cancellationToken = default)
        => client.GetTextAsync("/api/error_log", cancellationToken);


    public static Task<Stream?> GetCameraSnapshotAsync(this IHaRestClient client, string entityId,
        int? width = default, int? height = default, CancellationToken cancellationToken = default)
    {
        StringBuilder sb = new("/api/camera_proxy/");
        sb.Append(entityId);

        NameValueCollection query = new();
        if (width is not null)
            query.Add("width", width.ToString());
        if (height is not null)
            query.Add("height", height.ToString());

        string? s = query.ToString();

        if (!string.IsNullOrEmpty(s))
        {
            sb.Append('?');
            sb.Append(s);
        }

        return client.GetStreamAsync(sb.ToString(), cancellationToken);

    }

    public static Task<Stream?> GetCameraStreamAsync(this IHaRestClient client, string entityId,
        TimeSpan? interval, CancellationToken cancellationToken = default)
    {
        StringBuilder sb = new("/api/camera_proxy_stream/");
        sb.Append(entityId);

        NameValueCollection query = new();
        if (interval is not null)
            query.Add("interval", interval?.TotalSeconds.ToString());

        string? s = query.ToString();

        if (!string.IsNullOrEmpty(s))
        {
            sb.Append('?');
            sb.Append(s);
        }
        return client.GetStreamAsync(sb.ToString(), cancellationToken);
    }

    public static Task<IReadOnlyList<HaApiCalendar>?> GetCalendarsAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.GetAsync<IReadOnlyList<HaApiCalendar>>("/api/calendars", cancellationToken);

    public static Task<IReadOnlyList<HaApiCalendarEvent>?> GetCalendarEventAsync(this IHaRestClient client, string entityId,
       DateTime start, DateTime end, CancellationToken cancellationToken = default)
        => client.GetAsync<IReadOnlyList<HaApiCalendarEvent>>($"/api/calendars/{entityId}?start={start:o}&end={end:o}",
            cancellationToken);

    public static Task<HaEntityState?> SetStateAsync(this IHaRestClient client, string entityId, string state,
        object? attributes, CancellationToken cancellationToken)
        => client.PostAsync<HaState, HaEntityState>($"/api/states/{entityId}",
            new HaState() { State = state, Attributes = attributes }, cancellationToken);

    public static Task<HaApiState?> FireEventAsync(this IHaRestClient client, string eventType, object? eventData,
        CancellationToken cancellationToken = default)
         => client.PostAsync<Object, HaApiState>($"/api/events/{eventType}", eventData, cancellationToken);

    public static Task<HaApiState?> FireStateChangedAsync(this IHaRestClient client, HaStateChangeData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("state_changed", data, cancellationToken);

    public static Task<HaApiState?> FireConfigUpdatedAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("core_config_updated", null, cancellationToken);

    public static Task<HaApiState?> FireHomeAssistantStartAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("homeassistant_start", null, cancellationToken);

    public static Task<HaApiState?> FireHomeAssistantStartedAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("homeassistant_started", null, cancellationToken);

    public static Task<HaApiState?> FireHomeAssistantStoppingAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("homeassistant_stop", null, cancellationToken);

    public static Task<HaApiState?> FireHomeAssistantStoppingWriteAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("homeassistant_final_write", null, cancellationToken);

    public static Task<HaApiState?> FireHomeAssistantStoppedAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("homeassistant_stopped", null, cancellationToken);

    public static Task<HaApiState?> FireConfigEntryDiscoveredAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("config_entry_discovered", null, cancellationToken);

    public static Task<HaApiState?> FireServiceRegisteredAsync(this IHaRestClient client, HaServiceData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("service_registered", data, cancellationToken);

    public static Task<HaApiState?> FireServiceRemovedAsync(this IHaRestClient client, HaServiceData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("service_removed", data, cancellationToken);

    public static Task<HaApiState?> FireServiceCallAsync(this IHaRestClient client, HaCallServiceData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("call_service", data, cancellationToken);

    public static Task<HaApiState?> FireFlowProgressAsync(this IHaRestClient client, HaFlowProgressData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("data_entry_flow_progressed", data, cancellationToken);

    public static Task<HaApiState?> FireComponentLoadedAsync(this IHaRestClient client, HaComponentData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("component_loaded", data, cancellationToken);

    public static Task<HaApiState?> FireUserAddedAsync(this IHaRestClient client, HaUserData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("user_added", data, cancellationToken);

    public static Task<HaApiState?> FireUserRemovedAsync(this IHaRestClient client, HaUserData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("user_removed", data, cancellationToken);

    public static Task<HaApiState?> FireUserUpdatedAsync(this IHaRestClient client, HaUserData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("user_updated", data, cancellationToken);

    public static Task<HaApiState?> FireAutomationReloadedAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("automation_reloaded", null, cancellationToken);

    public static Task<HaApiState?> FireAutomationTriggeredAsync(this IHaRestClient client, HaAutomationData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("automation_triggered", data, cancellationToken);

    public static Task<HaApiState?> FireDeviceTrackerFoundAsync(this IHaRestClient client, HaDeviceTrackerData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("device_tracker_new_device", data, cancellationToken);

    public static Task<HaApiState?> FirePanelsUpdatedAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("panels_updated", null, cancellationToken);

    public static Task<HaApiState?> FireThemesUpdatedAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("themes_updated", null, cancellationToken);

    public static Task<HaApiState?> FireSceneReloadedAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("scene_reloaded", null, cancellationToken);

    public static Task<HaApiState?> FireFaceDetectedAsync(this IHaRestClient client, HaFaceData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("image_processing.detect_face", data, cancellationToken);

    public static Task<HaApiState?> FireLogbookEntryAddedAsync(this IHaRestClient client, HaLogbookData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("logbook_entry", data, cancellationToken);

    public static Task<HaApiState?> FireLovelaceUpdatedAsync(this IHaRestClient client, HaLovelaceData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("lovelace_updated", data, cancellationToken);

    public static Task<HaApiState?> FirePersistentNotificationsUpdateAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("persistent_notifications_updated", null, cancellationToken);

    public static Task<HaApiState?> FireScriptStartedAsync(this IHaRestClient client, HaScriptData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("script_started", data, cancellationToken);

    public static Task<HaApiState?> FireShoppingListUpdatedAsync(this IHaRestClient client, HaShoppingData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("shopping_list_updated", data, cancellationToken);

    public static Task<HaApiState?> FireTagScannedAsync(this IHaRestClient client, HaTagData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("shopping_list_updated", data, cancellationToken);

    public static Task<HaApiState?> FireTemplateReloadedAsync(this IHaRestClient client,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("event_template_reloaded", null, cancellationToken);


    public static Task<HaApiState?> FireTimerStartedAsync(this IHaRestClient client, HaTimerData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("timer.started", data, cancellationToken);

    public static Task<HaApiState?> FireTimerRestartedAsync(this IHaRestClient client, HaTimerData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("timer.restarted", data, cancellationToken);

    public static Task<HaApiState?> FireTimerCancelledAsync(this IHaRestClient client, HaTimerData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("timer.cancelled", data, cancellationToken);

    public static Task<HaApiState?> FireTimerFinishedAsync(this IHaRestClient client, HaTimerData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("timer.finished", data, cancellationToken);

    public static Task<HaApiState?> FireAreasUpdatedAsync(this IHaRestClient client, HaAreaData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("area_registry_updated", data, cancellationToken);

    public static Task<HaApiState?> FireDevicesUpdatedAsync(this IHaRestClient client, HaDeviceData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("device_registry_updated", data, cancellationToken);

    public static Task<HaApiState?> FireEntitysUpdatedAsync(this IHaRestClient client, HaEntityData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("entity_registry_updated", data, cancellationToken);

    public static Task<HaApiState?> FireDownloadCompletedAsync(this IHaRestClient client, HaDownloadData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("downloader_download_completed", data, cancellationToken);

    public static Task<HaApiState?> FireDownloadFailedAsync(this IHaRestClient client, HaDownloadData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("downloader_download_failed", data, cancellationToken);

    public static Task<HaApiState?> FireFolderChangedAsync(this IHaRestClient client, HaFolderData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("folder_watcher", data, cancellationToken);

    public static Task<HaApiState?> FireSystemLogUpdatedAsync(this IHaRestClient client, HaLogEntryData data,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync("system_log_event", data, cancellationToken);

    public static Task<HaApiState?> FireDomainReloadedAsync(this IHaRestClient client, string domain,
        CancellationToken cancellationToken = default)
        => client.FireEventAsync($"event_{domain}_reloaded", null, cancellationToken);

    public static Task<IReadOnlyList<HaEntityState>?> CallServiceAsync(this IHaRestClient client, string domain,
        string service, Object? data = default,
        CancellationToken cancellationToken = default)
        => client.PostAsync<Object, IReadOnlyList<HaEntityState>>($"/api/services/{domain}/{service}",
            data, cancellationToken);

    public static Task<string?> RenderTemplateAsync(this IHaRestClient client, string template,
        IDictionary<string, object>? variables, CancellationToken cancellationToken = default)
        => client.PostTextAsync($"/api/template", new
        {
            template,
            variables
        }, cancellationToken);

    public static Task<Object?> HandleIntentAsync(this IHaRestClient client, HaIntentData data,
        CancellationToken cancellationToken = default)
        => client.PostAsync<HaIntentData, Object>("/api/intent/handle/", data, cancellationToken);

    public static Task<IReadOnlyList<HaEvent>?> GetEventsStreamAsync(this IHaRestClient client,
        IEnumerable<string>? restrict, CancellationToken cancellationToken = default)
    {

        string api = restrict is not null && restrict.Any()
            ? "/api/stream?restrict=" + string.Join(',', restrict) : "/api/stream";

        return client.GetAsync<IReadOnlyList<HaEvent>>(api, cancellationToken);
    }

    public static Task<IReadOnlyList<string>?> GetComponentsAsync(this IHaRestClient client, HaIntentData data,
        CancellationToken cancellationToken = default)
        => client.GetAsync<IReadOnlyList<string>>("/api/components", cancellationToken);
}
