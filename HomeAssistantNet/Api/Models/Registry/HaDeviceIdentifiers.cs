using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

[JsonConverter(typeof(JsonDeviceIdentifierConverter))]
public sealed record HaDeviceIdentifier
{
    public string? Domain { get; init; }
    public string? Identifier { get; init; }
}
