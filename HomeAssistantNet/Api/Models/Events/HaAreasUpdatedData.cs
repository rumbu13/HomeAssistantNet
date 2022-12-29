using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaAreasUpdatedData
{
    public string? Action { get; init; }
    public string? AreaId { get; init; }
}
