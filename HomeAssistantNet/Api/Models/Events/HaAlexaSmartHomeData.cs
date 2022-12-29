using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaAlexaSmartHomeData
{
    public JsonElement? Request { get; init; }
    public HaAlexaSmartHomeResponse? Response { get; init; }
}
