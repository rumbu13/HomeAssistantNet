using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaCallServiceData
{
    public string? Domain { get; init; }
    public string? Service { get; init; }
    public JsonElement? Data { get; init; }
}
