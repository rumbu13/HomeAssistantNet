using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

[JsonConverter(typeof(JsonDeviceConnectionConverter))]
public sealed record HaDeviceConnection
{
    public string? ConnectionType { get; init; }
    public string? ConnectionIdentifier { get; init; }
}
