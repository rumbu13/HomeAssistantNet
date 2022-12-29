using HomeAssistantNet.Tools;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Json;

internal class JsonOneOrManyAlwaysStringConverter : JsonConverter<string[]>
{

    public override string[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    { 
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            List<string> list = new();
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                list.Add(HaTools.DefaultJsonAlwaysStringConverter.Read(ref reader, typeToConvert, options) ?? string.Empty);
            return list.ToArray();
        }

        return new string[] { HaTools.DefaultJsonAlwaysStringConverter.Read(ref reader, typeToConvert, options) ?? string.Empty };     
    }


    public override void Write(Utf8JsonWriter writer, string[] value, JsonSerializerOptions options)
    {
        if (value.Length == 1)
            JsonSerializer.Serialize(writer, value[0], options);
        else
            JsonSerializer.Serialize(writer, value, options);
    }
}
