using HomeAssistantNet.Api;
using HomeAssistantNet.Tools;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Json;

internal class JsonDeviceConnectionConverter : JsonConverter<HaDeviceConnection>
{
    public override HaDeviceConnection? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException();
        reader.Read();
        var s1 = reader.GetString();
        reader.Read();
        var s2 = reader.GetString();
        reader.Read();
        if (reader.TokenType != JsonTokenType.EndArray)
            throw new JsonException();

        return new HaDeviceConnection() { ConnectionType = s1, ConnectionIdentifier = s2 };  
    }

    public override void Write(Utf8JsonWriter writer, HaDeviceConnection value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        writer.WriteStringValue(value.ConnectionType);
        writer.WriteStringValue(value.ConnectionIdentifier);
        writer.WriteEndArray();
    }
}


