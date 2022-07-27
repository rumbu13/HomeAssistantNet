using HomeAssistantNet.Api.Internal;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaDeviceSelector
{
    public string? Integration { get; init; }
    public string? Manufacturer { get; init; }
    public string? Model { get; init; }
    public HaEntitySelector? Entity { get; init; }
    public bool? Multiple { get; init; }
}