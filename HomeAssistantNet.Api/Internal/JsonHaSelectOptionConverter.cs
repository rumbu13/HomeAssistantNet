using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeAssistantNet.Api.Internal;

internal class JsonHaSelectOptionConverter : JsonConverter<HaSelectOption>
{
    public override HaSelectOption? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    => reader.TokenType switch
    {
        JsonTokenType.StartObject => JsonSerializer.Deserialize<HaSelectOption>(ref reader, options),
        JsonTokenType.String => new HaSelectOption() { Value = JsonSerializer.Deserialize<string>(ref reader, options) },
        _ => throw new JsonException()
    };

    public override void Write(Utf8JsonWriter writer, HaSelectOption value, JsonSerializerOptions options)
    {
        if (value.Label is not null)
            JsonSerializer.Serialize(writer, value, options);
        else
            JsonSerializer.Serialize(writer, value.Value, options);
    }
}
