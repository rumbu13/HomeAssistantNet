using System.Text.Json;

namespace HomeAssistantNet.Api;

public sealed record HaEnergyValidationIssue
{
    public string? Type { get; init; }
    public string? Identifier { get; init; }
    public JsonElement? Value { get; init; }
}
