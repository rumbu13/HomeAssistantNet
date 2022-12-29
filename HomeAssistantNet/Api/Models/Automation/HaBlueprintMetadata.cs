using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaBlueprintMetadata
{
    public string? Domain { get; init; }
    public string? Name { get; init; }
    IDictionary<string, HaBlueprintInput>? Input { get; init; }
    public string? Description { get; init; }
    public string? SourceUrl { get; init; }
}
