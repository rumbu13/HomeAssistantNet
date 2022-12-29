using HomeAssistantNet.Api;
using HomeAssistantNet.Tools;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Json;

internal class JsonEventConverter : JsonConverter<HaEvent>
{

    public override HaEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Utf8JsonReader forwardReader = reader;
        if (forwardReader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        string? eventType = null;
        while (forwardReader.Read() && forwardReader.TokenType != JsonTokenType.EndObject)
        {
            switch (forwardReader.GetString())
            {
                case "event_type":              
                    forwardReader.Read();
                    eventType = forwardReader.GetString();
                    goto while_end;
                case "variables":
                    forwardReader.Read();
                    Utf8JsonReader varReader = forwardReader;
                    if (varReader.TokenType == JsonTokenType.StartObject)
                    {
                        varReader.Read();
                        if (varReader.TokenType == JsonTokenType.String && varReader.GetString() == "trigger")
                            return JsonSerializer.Deserialize<HaTriggerEvent>(ref reader, options);
                    }
                    JsonSerializer.Deserialize<JsonElement>(ref forwardReader, options);
                    break;
                case "event":
                    return JsonSerializer.Deserialize<HaSupervisorEvent>(ref reader, options);
                case "topic":
                case "payload":
                case "qos":
                case "retain":
                    return JsonSerializer.Deserialize<HaMqttEvent>(ref reader, options);
                case "events":
                case "start_time":
                case "end_time":
                case "partial":
                    return JsonSerializer.Deserialize<HaLogbookStreamEvent>(ref reader, options);
                default:
                    forwardReader.Read();
                    JsonSerializer.Deserialize<JsonElement>(ref forwardReader, options);
                    break;                    
            }
        }

    while_end:

        if (eventType == null)
            return JsonSerializer.Deserialize<HaUnknownEvent>(ref forwardReader, options);

        HaEvent? ev = eventType switch
        {
            "state_changed" => JsonSerializer.Deserialize<HaStateChangedEvent>(ref reader, options),
            "call_service" => JsonSerializer.Deserialize<HaCallServiceEvent>(ref reader, options),
            "area_registry_updated" => JsonSerializer.Deserialize<HaAreasUpdatedEvent>(ref reader, options),
            "device_registry_updated" => JsonSerializer.Deserialize<HaDevicesUpdatedEvent>(ref reader, options),
            "entity_registry_updated" => JsonSerializer.Deserialize<HaEntitiesUpdatedEvent>(ref reader, options),
            "config_entry_discovered" => JsonSerializer.Deserialize<HaConfigEntryDiscoveredEvent>(ref reader, options),
            "core_config_updated" => JsonSerializer.Deserialize<HaCoreConfigUpdatedEvent>(ref reader, options),
            "homeassistant_start" => JsonSerializer.Deserialize<HaStartEvent>(ref reader, options),
            "homeassistant_started" => JsonSerializer.Deserialize<HaStartedEvent>(ref reader, options),
            "homeassistant_stop" => JsonSerializer.Deserialize<HaStopEvent>(ref reader, options),
            "homeassistant_final_write" => JsonSerializer.Deserialize<HaFinalWriteEvent>(ref reader, options),
            "homeassistant_close" => JsonSerializer.Deserialize<HaCloseEvent>(ref reader, options),
            "service_registered" => JsonSerializer.Deserialize<HaServiceRegisteredEvent>(ref reader, options),
            "service_removed" => JsonSerializer.Deserialize<HaServiceRemovedEvent>(ref reader, options),
            "data_entry_flow_progressed" => JsonSerializer.Deserialize<HaDataEntryFlowProgressedEvent>(ref reader, options),
            "component_loaded" => JsonSerializer.Deserialize<HaComponentLoadedEvent>(ref reader, options),

            "user_added" => JsonSerializer.Deserialize<HaUserAddedEvent>(ref reader, options),
            "user_removed" => JsonSerializer.Deserialize<HaUserRemovedEvent>(ref reader, options),
            "user_updated" => JsonSerializer.Deserialize<HaUserUpdatedEvent>(ref reader, options),
            "automation_reloaded" => JsonSerializer.Deserialize<HaAutomationReloadedEvent>(ref reader, options),
            "automation_triggered" => JsonSerializer.Deserialize<HaAutomationTriggeredEvent>(ref reader, options),
            "panels_updated" => JsonSerializer.Deserialize<HaPanelsUpdatedEvent>(ref reader, options),
            "themes_updated" => JsonSerializer.Deserialize<HaThemesUpdatedEvent>(ref reader, options),
            "scene_reloaded" => JsonSerializer.Deserialize<HaSceneReloadedEvent>(ref reader, options),
            "image_processing.detect_face" => JsonSerializer.Deserialize<HaFaceDetectedEvent>(ref reader, options),
            "image_processing.found_plate" => JsonSerializer.Deserialize<HaPlateDetectedEvent>(ref reader, options),
            "logbook_entry" => JsonSerializer.Deserialize<HaLogbookEntryEvent>(ref reader, options),
            "lovelace_updated" => JsonSerializer.Deserialize<HaLovelaceUpdatedEvent>(ref reader, options),
            "mailbox_updated" => JsonSerializer.Deserialize<HaMailboxUpdatedEvent>(ref reader, options),
            "event_mqtt_reloaded" => JsonSerializer.Deserialize<HaMailboxUpdatedEvent>(ref reader, options),
            "persistent_notifications_updated" => JsonSerializer.Deserialize<HaPersistentNotificationsUpdatedEvent>(ref reader, options),
            "repairs_issue_registry_updated" => JsonSerializer.Deserialize<HaRepairsUpdatedEvent>(ref reader, options),
            "script_started" => JsonSerializer.Deserialize<HaScriptStartedEvent>(ref reader, options),
            "shoping_list_updated" => JsonSerializer.Deserialize<HaShoppingListUpdatedEvent>(ref reader, options),
            "tag_scanned" => JsonSerializer.Deserialize<HaTagScannedEvent>(ref reader, options),
            "timer.started" => JsonSerializer.Deserialize<HaTimerStartedEvent>(ref reader, options),
            "timer.restarted" => JsonSerializer.Deserialize<HaTimerRestartedEvent>(ref reader, options),
            "timer.paused" => JsonSerializer.Deserialize<HaTimerPausedEvent>(ref reader, options),
            "timer.cancelled" => JsonSerializer.Deserialize<HaTimerCancelledEvent>(ref reader, options),
            "timer.finished" => JsonSerializer.Deserialize<HaTimerFinishedEvent>(ref reader, options),
            "downloader_download_completed" => JsonSerializer.Deserialize<HaDownloadCompletedEvent>(ref reader, options),
            "downloader_download_failed" => JsonSerializer.Deserialize<HaDownloadFailedEvent>(ref reader, options),
            "folder_watcher" => JsonSerializer.Deserialize<HaFolderWatcherEvent>(ref reader, options),
            "system_log_event" => JsonSerializer.Deserialize<HaLogEntryEvent>(ref reader, options),

            "alexa_smart_home" => JsonSerializer.Deserialize<HaAlexaSmartHomeEvent>(ref reader, options),
            "arcam_fmj.turn_on" => JsonSerializer.Deserialize<HaArcamTurnOnEvent>(ref reader, options),
            "amcrest" => JsonSerializer.Deserialize<HaAmcrestEvent>(ref reader, options),
            "button_pressed" => JsonSerializer.Deserialize<HaEnoceanButtonPressedEvent>(ref reader, options),
            "deconz_event" => JsonSerializer.Deserialize<HaDeconzEvent>(ref reader, options),
            "deconz_alarm_event" => JsonSerializer.Deserialize<HaDeconzAlarmEvent>(ref reader, options),
            "device_tracker_new_device" => JsonSerializer.Deserialize<HaDeviceTrackerEvent>(ref reader, options),
            "doorbird_reset_favorites" => JsonSerializer.Deserialize<HaDoorbirdResetEvent>(ref reader, options),
            "dynalite_packet" => JsonSerializer.Deserialize<HaDynalitePacketEvent>(ref reader, options),
            "dynalite_preset" => JsonSerializer.Deserialize<HaDynalitePresetEvent>(ref reader, options),
            "ecovacs_error" => JsonSerializer.Deserialize<HaEcovacsErrorEvent>(ref reader, options),
            "flic_click" => JsonSerializer.Deserialize<HaFlicClickEvent>(ref reader, options),
            "foursquare.checkin" => JsonSerializer.Deserialize<HaFoursquareCheckinEvent>(ref reader, options),
            "goal" => JsonSerializer.Deserialize<HaGoalEvent>(ref reader, options),
            "hdmi_cec_unavailable" => JsonSerializer.Deserialize<HaHdmiCecUnavailableEvent>(ref reader, options),
            "homematic.error" => JsonSerializer.Deserialize<HaHomematicErrorEvent>(ref reader, options),
            "homematic.keypress" => JsonSerializer.Deserialize<HaHomematicKeypressEvent>(ref reader, options),
            "homematic.impulse" => JsonSerializer.Deserialize<HaHomematicImpulseEvent>(ref reader, options),
            "idteck_prox_keycard" => JsonSerializer.Deserialize<HaIdteckProximityEvent>(ref reader, options),
            "isy994_control" => JsonSerializer.Deserialize<HaIssy994ControlEvent>(ref reader, options),
            _ => null
        };

        if (ev != null)
            return ev;

        if (eventType.StartsWith("event_", StringComparison.Ordinal)
            && eventType.EndsWith("_reloaded", StringComparison.Ordinal))
            return JsonSerializer.Deserialize<HaDomainReloadedEvent>(ref reader, options);

        if (eventType.StartsWith("doorbird_", StringComparison.Ordinal))
            return JsonSerializer.Deserialize<HaDoorbirdEvent>(ref reader, options);

        if (eventType.StartsWith("abode_", StringComparison.Ordinal))
            return JsonSerializer.Deserialize<HaAbodeEvent>(ref reader, options);

        if (eventType.StartsWith("html5_notification.", StringComparison.Ordinal))
            return JsonSerializer.Deserialize<HaHtml5NotificationEvent>(ref reader, options);


        return JsonSerializer.Deserialize<HaUnknownStandardEvent>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, HaEvent value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(value, value.GetType(), options);
    }
}
