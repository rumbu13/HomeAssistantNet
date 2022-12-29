using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaEntity
{
    public string? EntityId { get; init; }
    public string? AreaId { get; init; }
    public string? ConfigEntryId { get; init; }
    public string? DeviceClass { get; init; }
    public string? DeviceId { get; init; }
    public string? DisabledBy { get; init; }
    public string? EntityCategory { get; init; }
    public string? HiddenBy { get; init; }
    public string? Icon { get; init; }
    public bool? HasEntityName { get; init; }
    public string? OriginalDeviceClass { get; init; }
    public string? OriginalIcon { get; init; }
    public string? OriginalName { get; init; }
    public int? SupportedFeatures { get; init; }
    public string? UnitOfMeasurement { get; init; }
    public string? Platform { get; init; }
    public string? UniqueId { get; init; }
    public IDictionary<string, IDictionary<string, JsonElement>>? Options { get; init; }
    public IDictionary<string, JsonElement>? Capabilities { get; init; }
}
