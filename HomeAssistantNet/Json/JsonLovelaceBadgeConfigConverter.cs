using HomeAssistantNet.Api;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Json;

internal class JsonLovelaceBadgeConfigConverter : JsonConverter<HaLovelaceBadgeConfig>
{
    public override HaLovelaceBadgeConfig? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
            return new HaLovelaceBadgeConfig()
            {
                Type = reader.GetString(),
            };
        else if (reader.TokenType == JsonTokenType.StartObject)
            return JsonSerializer.Deserialize<HaLovelaceBadgeConfig>(ref reader, options);

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, HaLovelaceBadgeConfig value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize<HaLovelaceBadgeConfig>(writer, value, options);
    }
}
