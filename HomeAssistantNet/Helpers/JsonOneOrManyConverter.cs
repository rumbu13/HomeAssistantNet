using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeAssistantNet.Helpers;

internal class JsonOneOrManyConverter<T> : JsonConverter<IReadOnlyCollection<T>>
{
    public override IReadOnlyCollection<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => reader.TokenType switch
        {
            JsonTokenType.StartArray => JsonSerializer.Deserialize<IReadOnlyCollection<T>>(ref reader, options),
            _ => new List<T>() { JsonSerializer.Deserialize<T>(ref reader, options)! }
        };


    public override void Write(Utf8JsonWriter writer, IReadOnlyCollection<T> value, JsonSerializerOptions options)
    {
        if (value.Count == 1)
            JsonSerializer.Serialize(writer, value.First(), options);
        else
            JsonSerializer.Serialize(writer, value, options);
    }
}
