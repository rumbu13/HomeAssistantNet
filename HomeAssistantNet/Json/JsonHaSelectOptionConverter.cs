using HomeAssistantNet.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeAssistantNet.Json;

internal class JsonHaSelectOptionConverter : JsonConverter<HaSelectOption>
{
    public override HaSelectOption? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {

        if (reader.TokenType == JsonTokenType.String)
        {
            var v = reader.GetString();
            return new HaSelectOption() { Label = v, Value = v };
        }
        else if (reader.TokenType == JsonTokenType.StartObject)
        {
            string? label = null, value = null;
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.GetString() == "label")
                {
                    reader.Read();
                    label = reader.GetString();
                }
                else if (reader.GetString() == "value")
                {
                    reader.Read();
                    value = reader.GetString();
                }
            }
            return new HaSelectOption() { Label = label, Value = value };
        }
        throw new JsonException();
    }   

    public override void Write(Utf8JsonWriter writer, HaSelectOption value, JsonSerializerOptions options)
    {
        if (value.Label is not null)
            JsonSerializer.Serialize(writer, value, options);
        else
            JsonSerializer.Serialize(writer, value.Value, options);
    }
}
