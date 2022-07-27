using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeAssistantNet.Helpers;

internal class JsonDateOrTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();
        reader.Read();
        var item = reader.GetString();
        if (item != "date" && item != "dateTime")
            throw new JsonException();
        reader.Read();
        return JsonSerializer.Deserialize<DateTime>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("dateTime", value.ToString("o"));
        writer.WriteEndObject();
    }
}
