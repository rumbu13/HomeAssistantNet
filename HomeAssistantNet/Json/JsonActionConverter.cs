using HomeAssistantNet.Api;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Json;

internal class JsonActionConverter : JsonConverter<HaAction>
{
    public override HaAction? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Utf8JsonReader forwardReader = reader;
        if (forwardReader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        while (forwardReader.Read() && forwardReader.TokenType != JsonTokenType.EndObject)
        {
            switch (forwardReader.GetString())
            {
                case "event":
                case "event_data":
                case "event_data_template":
                    return JsonSerializer.Deserialize<HaEventAction>(ref reader, options);
                case "service":
                    forwardReader.Read();
                    string? service = forwardReader.GetString();
                    if (service == "scene.turn_on")
                        return JsonSerializer.Deserialize<HaServiceSceneAction>(ref reader, options);
                    else if (service == "media_player.play_media")
                        return JsonSerializer.Deserialize<HaPlayMediaAction>(ref reader, options);
                    else
                        return JsonSerializer.Deserialize<HaServiceAction>(ref reader, options);
                case "service_template":                
                case "target":
                    return JsonSerializer.Deserialize<HaServiceAction>(ref reader, options);
                case "type":
                case "device_id":
                case "domain":
                    return JsonSerializer.Deserialize<HaDeviceAction>(ref reader, options);
                case "delay":
                    return JsonSerializer.Deserialize<HaDelayAction>(ref reader, options);
                case "scene":
                    return JsonSerializer.Deserialize<HaLegacySceneAction>(ref reader, options);
                case "wait_template":
                    return JsonSerializer.Deserialize<HaWaitAction>(ref reader, options);
                case "wait_for_trigger":
                    return JsonSerializer.Deserialize<HaWaitTriggerAction>(ref reader, options);
                case "repeat":
                    return JsonSerializer.Deserialize<HaRepeatAction>(ref reader, options);
                case "count":
                    return JsonSerializer.Deserialize<HaCountRepeatAction>(ref reader, options);
                case "while":
                    return JsonSerializer.Deserialize<HaWhileRepeatAction>(ref reader, options);
                case "until":
                    return JsonSerializer.Deserialize<HaUntilRepeatAction>(ref reader, options);
                case "for_each":
                    return JsonSerializer.Deserialize<HaForEachRepeatAction>(ref reader, options);
                case "conditions":
                    return JsonSerializer.Deserialize<HaChooseActionChoice>(ref reader, options);
                case "choose":
                    return JsonSerializer.Deserialize<HaChooseAction>(ref reader, options);
                case "if":
                case "then":
                case "else":
                    return JsonSerializer.Deserialize<HaIfAction>(ref reader, options);
                case "variables":
                    return JsonSerializer.Deserialize<HaVariablesAction>(ref reader, options);
                case "stop":
                case "error":
                    return JsonSerializer.Deserialize<HaStopAction>(ref reader, options);
                case "parallel":
                    return JsonSerializer.Deserialize<HaParallelAction>(ref reader, options);
                default:
                    forwardReader.Read();
                    JsonSerializer.Deserialize<JsonElement>(ref forwardReader, options);
                    break;                    
            }
        }

        return JsonSerializer.Deserialize<HaUnknowAction>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, HaAction value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(value, value.GetType(), options);
    }
}
