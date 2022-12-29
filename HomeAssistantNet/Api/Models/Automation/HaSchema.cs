using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaSchema
{
    public string? Name { get; init; }
    public JsonElement? Default { get; init; }
    public bool? Required { get; init; }
    public HaSchemaDescription? Description {get; init;}
    public IDictionary<string, string>? Context { get; init; }
    public string? Type { get; init; }
    public string? ColumnMinWidth { get; init; }
    public HaSchema? Schema { get; init; }
    public HaSelector? Selector { get; init; }
    public string? Value { get; init; }

    [JsonPropertyName("valueMin")]
    public double? ValueMin { get; init; }

    [JsonPropertyName("valueMax")]
    public double? ValueMax { get; init; }

    public JsonElement? Options { get; init; }
    public string? Format { get; init; }

}
