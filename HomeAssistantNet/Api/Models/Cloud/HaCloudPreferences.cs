using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaCloudPreferences
{
    public bool? GoogleEnabled { get; init; }
    public bool? AlexaEnabled { get; init; }
    public bool? RemoteEnabled { get; init; }
    public string? GoogleSecureDevicesPin { get; init; }

    [JsonConverter(typeof(JsonOneOrManyConverter<HaCloudwebhook>))]
    public HaCloudwebhook[]? Cloudhooks { get; init; }

    public string[]? GoogleDefaultExpose { get; init; }

    [JsonConverter(typeof(JsonOneOrManyConverter<HaGoogleEntityConfig>))]
    public HaGoogleEntityConfig[]? GoogleEntityConfigs { get; init; }

    public string[]? AlexaDefaultExpose { get; init; }

    [JsonConverter(typeof(JsonOneOrManyConverter<HaAlexaEntityConfig>))]
    public HaAlexaEntityConfig[]? AlexaEntityConfigs { get; init; }

    public bool? AlexaReportState { get; init; }
    public bool? GoogleReportState { get; init; }
    public string[][]? TtsDefaultVoice { get; init; }

}
