using HomeAssistantNet.Client;
using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaCalendarData
{
    public DateTime? Start { get; init; }
    public DateTime? End { get; init; }
    public string? Summary { get; init; }
    public bool? AllDay { get; init; }
    public string? Description { get; init; }
    public string? Location { get; init; }
}
