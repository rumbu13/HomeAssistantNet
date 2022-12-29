using HomeAssistantNet.Api;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Json;

internal class JsonConditionConverter : JsonConverter<HaCondition>
{
    public override HaCondition? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Utf8JsonReader forwardReader = reader;
        if (forwardReader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        string? condition = null;
        while (forwardReader.Read() && forwardReader.TokenType != JsonTokenType.EndObject)
        {
            switch (forwardReader.GetString())
            {
                case "condition":
                    forwardReader.Read();
                    condition = forwardReader.GetString();
                    goto while_break;
                case "conditions":
                    return JsonSerializer.Deserialize<HaLogicalCondition>(ref reader, options);
                case "for":
                    return JsonSerializer.Deserialize<HaStateCondition>(ref reader, options);
                case "above":
                case "below":
                    return JsonSerializer.Deserialize<HaNumericStateCondition>(ref reader, options);
                case "after_offset":
                case "before_offset":
                    return JsonSerializer.Deserialize<HaSunCondition>(ref reader, options);
                case "zone":
                    return JsonSerializer.Deserialize<HaZoneCondition>(ref reader, options);
                case "weekday":
                    return JsonSerializer.Deserialize<HaTimeCondition>(ref reader, options);
                case "id":
                    return JsonSerializer.Deserialize<HaTriggerCondition>(ref reader, options);
                default:
                    forwardReader.Read();
                    JsonSerializer.Deserialize<JsonElement>(ref forwardReader, options);
                    break;                    
            }
        }

    while_break:;

        return condition switch
        {
            "state" => JsonSerializer.Deserialize<HaStateCondition>(ref reader, options),
            "and" or "not" or "or" => JsonSerializer.Deserialize<HaLogicalCondition>(ref reader, options),
            "numeric_state" => JsonSerializer.Deserialize<HaNumericStateCondition>(ref reader, options),
            "sun" => JsonSerializer.Deserialize<HaSunCondition>(ref reader, options),
            "zone" => JsonSerializer.Deserialize<HaZoneCondition>(ref reader, options),
            "time" => JsonSerializer.Deserialize<HaTimeCondition>(ref reader, options),
            "template" => JsonSerializer.Deserialize<HaTemplateCondition>(ref reader, options),
            "trigger" => JsonSerializer.Deserialize<HaTriggerCondition>(ref reader, options),
            _ => throw new JsonException("Unknown condition type"),
        };
    }

    public override void Write(Utf8JsonWriter writer, HaCondition value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(value, value.GetType(), options);
    }
}
