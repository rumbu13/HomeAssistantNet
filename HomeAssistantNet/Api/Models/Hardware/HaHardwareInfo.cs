using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaHardwareInfo
{
    public HaHardwareInfoEntry[]? Hardware { get; init; }
}
