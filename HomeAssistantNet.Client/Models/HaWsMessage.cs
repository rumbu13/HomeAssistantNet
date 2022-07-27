using HomeAssistantNet.Client.Internal;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Client;

public record HaWsMessage
{
    public int? Id { get; init; }
    public string? Type { get; init; }
    public bool? Success { get; init; }
    public JsonElement? Result { get; init; }
    public HaEvent? Event { get; init; }
    public HaWsError? Error { get; init; }
    public string? HaVersion { get; init; }
    public string? Message { get; init; }
}
