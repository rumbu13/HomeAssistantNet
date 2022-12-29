using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaSchemaDescription
{
    public string? Suffix { get; init; }
    public JsonElement? SuggestedValue { get; set; }

}
