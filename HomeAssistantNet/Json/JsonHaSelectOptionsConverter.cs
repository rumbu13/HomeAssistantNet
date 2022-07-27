using HomeAssistantNet.Api;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Json;

internal class JsonHaSelectOptionsConverter : JsonConverter<HaSelectOption[]>
{
    public override HaSelectOption[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var converter = new JsonHaSelectOptionConverter();
        if (reader.TokenType != JsonTokenType.StartArray)
            throw new JsonException();
        var list = new List<HaSelectOption>();
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            list.Add(converter.Read(ref reader, typeof(HaSelectOption), options)!);
        return list.ToArray();
    }

    public override void Write(Utf8JsonWriter writer, HaSelectOption[] value, JsonSerializerOptions options)
    {
        if (value.Any(v => v.Label is not null))
            JsonSerializer.Serialize(writer, value, options);
        else
            JsonSerializer.Serialize(writer, value.Select(v => v.Value)!, options);
    }
}
