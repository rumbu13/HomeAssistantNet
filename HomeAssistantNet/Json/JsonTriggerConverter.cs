using HomeAssistantNet.Api;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Json;

internal class JsonTriggerConverter : JsonConverter<HaTrigger>
{
    public override HaTrigger? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Utf8JsonReader forwardReader = reader;
        if (forwardReader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        string? platform = null;
        while (forwardReader.Read() && forwardReader.TokenType != JsonTokenType.EndObject)
        {
            switch (forwardReader.GetString())
            {
                case "platform":
                    forwardReader.Read();
                    platform = forwardReader.GetString();
                    goto while_break;
                case "from":
                case "to":
                    return JsonSerializer.Deserialize<HaStateTrigger>(ref reader, options);
                case "topic":
                case "payload":
                    return JsonSerializer.Deserialize<HaMqttTrigger>(ref reader, options);
                case "source":
                    return JsonSerializer.Deserialize<HaGeolocationTrigger>(ref reader, options);
                case "above":
                case "below":
                    return JsonSerializer.Deserialize<HaNumericStateTrigger>(ref reader, options);
                case "offset":
                    return JsonSerializer.Deserialize<HaSunTrigger>(ref reader, options);
                case "hours":
                case "minutes":
                case "seconds":
                    return JsonSerializer.Deserialize<HaTimePatternTrigger>(ref reader, options);
                case "webhook_id":
                    return JsonSerializer.Deserialize<HaWebhookTrigger>(ref reader, options);
                case "tag_id":
                    return JsonSerializer.Deserialize<HaTagTrigger>(ref reader, options);
                case "at":
                    return JsonSerializer.Deserialize<HaTimeTrigger>(ref reader, options);
                case "event_type":
                case "event_data":
                    return JsonSerializer.Deserialize<HaEventTrigger>(ref reader, options);
                default:
                    forwardReader.Read();
                    JsonSerializer.Deserialize<JsonElement>(ref forwardReader, options);
                    break;                    
            }
        }

    while_break:;

        return platform switch
        {
            "state" => JsonSerializer.Deserialize<HaStateTrigger>(ref reader, options),
            "mqtt" => JsonSerializer.Deserialize<HaMqttTrigger>(ref reader, options),
            "geo_location" => JsonSerializer.Deserialize<HaGeolocationTrigger>(ref reader, options),
            "homeassistant" => JsonSerializer.Deserialize<HaHomeAssistantTrigger>(ref reader, options),
            "numeric_state" => JsonSerializer.Deserialize<HaNumericStateTrigger>(ref reader, options),
            "sun" => JsonSerializer.Deserialize<HaSunTrigger>(ref reader, options),
            "time_pattern" => JsonSerializer.Deserialize<HaTimePatternTrigger>(ref reader, options),
            "webhook" => JsonSerializer.Deserialize<HaWebhookTrigger>(ref reader, options),
            "zone" => JsonSerializer.Deserialize<HaZoneTrigger>(ref reader, options),
            "tag" => JsonSerializer.Deserialize<HaTagTrigger>(ref reader, options),
            "time" => JsonSerializer.Deserialize<HaTimeTrigger>(ref reader, options),
            "template" => JsonSerializer.Deserialize<HaTemplateTrigger>(ref reader, options),
            "event" => JsonSerializer.Deserialize<HaEventTrigger>(ref reader, options),
            "calendar" => JsonSerializer.Deserialize<HaCalendarTrigger>(ref reader, options),
            _ => throw new JsonException("Unknown trigger type"),
        };
    }

    public override void Write(Utf8JsonWriter writer, HaTrigger value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(value, value.GetType(), options);
    }
}
