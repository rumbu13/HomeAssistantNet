using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaHtml5NotificationEvent : HaStandardEvent
{
    public JsonElement? Data { get; init; }
}
