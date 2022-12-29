using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Json;

internal class JsonDurationConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var result = default(TimeSpan);

        if (reader.TokenType == JsonTokenType.String)
            return TimeSpan.Parse(reader.GetString()!, CultureInfo.InvariantCulture);

        if (reader.TokenType == JsonTokenType.Number)
            return TimeSpan.FromSeconds(reader.GetDouble());

        if (reader.TokenType != JsonTokenType.StartObject)
            throw new JsonException();

        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            switch (reader.GetString())
            {
                case "days":
                    reader.Read();
                    result = result.Add(TimeSpan.FromDays(reader.GetDouble()));
                    break;
                case "hours":
                    reader.Read();
                    result = result.Add(TimeSpan.FromHours(reader.GetDouble()));
                    break;
                case "minutes":
                    reader.Read();
                    result = result.Add(TimeSpan.FromMinutes(reader.GetDouble()));
                    break;
                case "seconds":
                    reader.Read();
                    result = result.Add(TimeSpan.FromSeconds(reader.GetDouble()));
                    break;
                case "milliseconds":
                    reader.Read();
                    result = result.Add(TimeSpan.FromMilliseconds(reader.GetDouble()));
                    break;
                default:
                    throw new JsonException();
            }
        }

        return result;
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        if (value.Days > 0)
            writer.WriteNumber("days", value.Days);
        if (value.Hours > 0)
            writer.WriteNumber("hours", value.Hours);
        if (value.Minutes > 0)
            writer.WriteNumber("minutes", value.Minutes);
        if (value.Seconds > 0)
            writer.WriteNumber("seconds", value.Seconds);
        if (value.Milliseconds > 0)
            writer.WriteNumber("milliseconds", value.Milliseconds);
        writer.WriteEndObject();

    }
}
