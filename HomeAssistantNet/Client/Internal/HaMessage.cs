using HomeAssistantNet.Api;
using System.Text.Json;

namespace HomeAssistantNet.Client;

internal sealed record HaMessage
{
    public int? Id { get; init; }
    public string? Type { get; init; }
    public bool? Success { get; init; }
    public JsonElement? Result { get; init; }
    public HaEvent? Event { get; init; }
    public HaError? Error { get; init; }
    public string? HaVersion { get; init; }
    public string? Message { get; init; }
}
