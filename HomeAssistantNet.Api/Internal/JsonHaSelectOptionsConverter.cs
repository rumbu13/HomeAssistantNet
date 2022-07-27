using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeAssistantNet.Api.Internal;

internal class JsonHaSelectOptionsConverter : JsonConverter<IReadOnlyList<HaSelectOption>>
{
    public override IReadOnlyList<HaSelectOption>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var converter = new JsonHaSelectOptionConverter();
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException();
        var list = new List<HaSelectOption>();
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            list.Add(converter.Read(ref reader, typeof(HaSelectOption), options)!);
        return list;
    }

    public override void Write(Utf8JsonWriter writer, IReadOnlyList<HaSelectOption> value, JsonSerializerOptions options)
    {
        if (value.Any(v => v.Label is not null))
            JsonSerializer.Serialize(writer, value, options);
        else
            JsonSerializer.Serialize(writer, value.Select(v => v.Value)!, options);
    }
}
