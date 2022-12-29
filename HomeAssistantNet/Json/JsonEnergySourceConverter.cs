using HomeAssistantNet.Api;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Json;

internal class JsonEnergySourceConverter : JsonConverter<HaEnergySource>
{
    public override HaEnergySource? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Utf8JsonReader forwardReader = reader;
        if (forwardReader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        while (forwardReader.Read() && forwardReader.TokenType != JsonTokenType.EndObject)
        {
            switch (forwardReader.GetString())
            {
                case "type":
                    forwardReader.Read();
                    string? type = forwardReader.GetString();
                    return type switch
                    {
                        "grid" => JsonSerializer.Deserialize<HaGridEnergySource>(ref reader, options),
                        "solar" => JsonSerializer.Deserialize<HaSolarEnergySource>(ref reader, options),
                        "battery" => JsonSerializer.Deserialize<HaBatteryEnergySource>(ref reader, options),
                        "gas" => JsonSerializer.Deserialize<HaGasEnergySource>(ref reader, options),
                        _ => JsonSerializer.Deserialize<HaUnknownEnergySource>(ref reader, options),
                    };
                case "flow_from":
                case "flow_to":
                case "cost_adjustment_day":
                    return JsonSerializer.Deserialize<HaGridEnergySource>(ref reader, options);
                case "config_entry_solar_forecast":
                    return JsonSerializer.Deserialize<HaSolarEnergySource>(ref reader, options);
                case "stat_cost":
                case "entity_energy_from":
                case "entity_energy_price":
                case "number_energy_price":
                    return JsonSerializer.Deserialize<HaGasEnergySource>(ref reader, options);
                
                default:
                    forwardReader.Read();
                    JsonSerializer.Deserialize<JsonElement>(ref forwardReader, options);
                    break;
            }
        }

        return JsonSerializer.Deserialize<HaUnknownEnergySource>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, HaEnergySource value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(value, value.GetType(), options);
    }
}
