using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaBlueprintInput
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public HaSelector? Selector { get; init; }
    public JsonElement? Default { get; init; }
}
