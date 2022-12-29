using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaHardwareBoard
{
    public string? Manufacturer { get; init; }
    public string? Model { get; init; }
    public string? Revision { get; init; }
    public string? HassioBoardId { get; init; }
}
