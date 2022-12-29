using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaDeconzAlarmData
{
    public string? Id { get; init; }
    public string? UniqueId { get; init; }
    public string? Event { get; init; }
}
