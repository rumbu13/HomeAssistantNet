using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaFeedreaderEvent : HaStandardEvent
{    
    public JsonElement? Data { get; init; }
}
