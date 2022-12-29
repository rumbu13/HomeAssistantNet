using HomeAssistantNet.Tools;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Json;

internal class JsonEnumConverter: JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {

        return (JsonConverter)Activator.CreateInstance(
                typeof(JsonEnumMemberConverter<>).MakeGenericType(
                    new Type[] { typeToConvert }),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: null,
                culture: null)!;
    }
}

internal class JsonEnumMemberConverter<T> : JsonConverter<T> where T : struct, Enum
{
    private Dictionary<string, string>? _readMap;
    private Dictionary<string, string>? _writeMap;

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }

    public void FillValueMap(JsonSerializerOptions options)
    {
        if (_readMap != null || _writeMap != null)
            return;

        _readMap = new Dictionary<string, string>();
        _writeMap = new Dictionary<string, string>();
        foreach (var v in Enum.GetValues<T>())
        {
            string name = Enum.GetName<T>(v)!;
            string json = v.GetAttributeOfEnumMember<JsonPropertyNameAttribute>()?.Name ??
                (options.PropertyNamingPolicy != null ? options.PropertyNamingPolicy.ConvertName(name) : name);
            _readMap[json] = name;
            _writeMap[name] = json;
        }
    }

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var s = reader.GetString();
        string? targetName = null;
        FillValueMap(options);

        if (!options.PropertyNameCaseInsensitive)
            _readMap!.TryGetValue(s!, out targetName);
        else
            targetName = _readMap!.FirstOrDefault(k =>
                string.Equals(k.Key, s, StringComparison.OrdinalIgnoreCase)).Value;

        if (targetName == null)
                throw new JsonException($"Invalid enum member: {s}");

        return Enum.Parse<T>(targetName, options.PropertyNameCaseInsensitive);
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        FillValueMap(options);

        writer.WriteStringValue(_writeMap![value.ToString()]);
    }


}
