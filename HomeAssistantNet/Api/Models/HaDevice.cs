using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaDevice
{
    public string? Id { get; init; }
    public string? ConfigurationUrl { get; init; }
    public string? DisabledBy { get; init; }
    public string? EntryType { get; init; }
    public string? Manufacturer { get; init; }
    public string? Model { get; init; }
    public string? Name { get; init; }
    public string? NameByUser { get; init; }
    public string? AreaId { get; init; }
    public string? SuggestedArea { get; init; }

    [JsonConverter(typeof(JsonAlwaysStringConverter))]
    public string? SwVersion { get; init; }

    [JsonConverter(typeof(JsonAlwaysStringConverter))]
    public string? HwVersion { get; init; }

    public string? ViaDeviceId { get; init; }
    public string[]? ConfigEntries { get; init; }
    public string[][]? Connections { get; init; }
    public string[][]? Identifiers { get; init; }


}
