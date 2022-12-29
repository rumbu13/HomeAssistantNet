using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaBlueprintImport
{
    public string? SuggestedFilename { get; init; }
    public string? RawData { get; init; }
    public HaBlueprint? Blueprint { get; init; }
    public string[]? ValidationErrors { get; init; }
}
