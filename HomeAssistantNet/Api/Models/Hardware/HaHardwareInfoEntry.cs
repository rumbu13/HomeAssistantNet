using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaHardwareInfoEntry
{
    public HaHardwareBoard? Board { get; init; }
    public string? Name { get; init; }
    public string? Url { get; init; }
}
