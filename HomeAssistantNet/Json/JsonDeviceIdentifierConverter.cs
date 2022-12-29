using HomeAssistantNet.Api;
using HomeAssistantNet.Tools;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Json;

internal class JsonDeviceIdentifierConverter : JsonConverter<HaDeviceIdentifier>
{
    public override HaDeviceIdentifier? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException();
        reader.Read();

        string? s1 = null, s2 = null;
        if (reader.TokenType == JsonTokenType.String)
        {
            s1 = reader.GetString();
            reader.Read();
        }
        if (reader.TokenType == JsonTokenType.String)
        {
            s2 = reader.GetString();
            reader.Read();
        }
        if (reader.TokenType != JsonTokenType.EndArray)
            throw new JsonException();

        return new HaDeviceIdentifier() { Domain = s1, Identifier = s2 };  
    }

    public override void Write(Utf8JsonWriter writer, HaDeviceIdentifier value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteStringValue(value.Domain);
        writer.WriteStringValue(value.Identifier);
        writer.WriteEndArray();
    }
}
