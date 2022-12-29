using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaAbodeData
{
    public string? DeviceId { get; init; }
    public string? DeviceName { get; init; }
    public string? DeviceType { get; init; }
    public string? EventCode { get; init; }
    public string? EventName { get; init; }
    public string? EventType { get; init; }
    public string? EventUtc { get; init; }
    public string? UserName { get; init; }
    public string? AppType { get; init; }
    public string? EventBy { get; init; }
    public string? Date { get; init; }
    public string? Time { get; init; }
}
