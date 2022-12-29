using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaDevice
{
    public string? Id { get; init; }
    public string? ConfigurationUrl { get; private set; }
    public HaDisabledBy? DisabledBy { get; init; }
    public string? EntryType { get; init; }
    public string? Manufacturer { get; private set; }
    public string? Model { get; private set; }
    public string? Name { get; init; }
    public string? NameByUser { get; init; }
    public string? AreaId { get; init; }
    public string? SuggestedArea { get; private set; }

    [JsonConverter(typeof(JsonAlwaysStringConverter))]
    public string? SwVersion { get; private set; }

    [JsonConverter(typeof(JsonAlwaysStringConverter))]
    public string? HwVersion { get; init; }

    public string? ViaDeviceId { get; init; }
    public string[]? ConfigEntries { get; init; }    
    public HaDeviceConnection[]? Connections { get; private set; }
    public HaDeviceIdentifier[]? Identifiers { get; private set; }

    [JsonPropertyName("cu")]
    private string? cu { get => ConfigurationUrl; set => ConfigurationUrl = value; }

    [JsonPropertyName("cns")]
    private HaDeviceConnection[]? cns { get => Connections; set => Connections = value; }

    [JsonPropertyName("ids")]
    private HaDeviceIdentifier[]? ids { get => Identifiers; set => Identifiers = value; }

    [JsonPropertyName("mf")]
    private string? mf { get => Manufacturer; set => Manufacturer = value; }

    [JsonPropertyName("mdl")]
    private string? mdl { get => Model; set => Model = value; }

    [JsonPropertyName("sw")]
    [JsonConverter(typeof(JsonAlwaysStringConverter))]
    private string? sw { get => SwVersion; set => SwVersion = value; }

    [JsonPropertyName("sa")]
    private string? sa { get => SuggestedArea; set => SuggestedArea = value; }
}
