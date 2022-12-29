using HomeAssistantNet.Api;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Json;

internal class JsonLovelaceViewConfigConverter : JsonConverter<HaLovelaceViewConfig[]>
{
    public override HaLovelaceViewConfig[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.True)
            return new HaLovelaceViewConfig[] { };
        if (reader.TokenType == JsonTokenType.False)
            return null;
        else if (reader.TokenType == JsonTokenType.StartArray)
            return JsonSerializer.Deserialize<HaLovelaceViewConfig[]>(ref reader, options);

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, HaLovelaceViewConfig[] value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize<HaLovelaceViewConfig[]>(writer, value, options);
    }
}
