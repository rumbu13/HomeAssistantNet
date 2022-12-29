using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaBlueprint
{
    public string? Error { get; init; }
    public HaBlueprintMetadata? Metadata { get; init; }
}
