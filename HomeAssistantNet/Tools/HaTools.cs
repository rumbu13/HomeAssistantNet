using HomeAssistantNet.Api;
using HomeAssistantNet.Json;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Tools
{
    public static class HaTools
    {

        private static JsonSerializerOptions? s_defaultJsonSerializerOptions;
        private static JsonNamingPolicy? s_defaultJsonNamingPolicy;
        private static JsonConverter? s_defaultJsonEnumConverter;
        private static JsonAlwaysStringConverter? s_defaultJsonAlwaysStringConverter;


        public static JsonSerializerOptions DefaultJsonSerializerOptions
            => s_defaultJsonSerializerOptions ?? CreateDefaultJsonSerializerOptions();

        public static JsonNamingPolicy? DefaultJsonNamingPolicy
            => s_defaultJsonNamingPolicy ?? CreateDefaultJsonNamingPolicy();

        public static JsonConverter DefaultJsonEnumConverter
            => s_defaultJsonEnumConverter ?? CreateDefaultJsonEnumConverter();

        public static JsonAlwaysStringConverter DefaultJsonAlwaysStringConverter
            => s_defaultJsonAlwaysStringConverter ?? CreateDefaultJsonAlwaysStringConverter();

        private static JsonAlwaysStringConverter CreateDefaultJsonAlwaysStringConverter()
        {
            var converter = new JsonAlwaysStringConverter();
            return Interlocked.CompareExchange(ref s_defaultJsonAlwaysStringConverter, converter, null) ?? converter;
        }

        private static JsonConverter CreateDefaultJsonEnumConverter()
        {
            var converter = new JsonEnumConverter();
            return Interlocked.CompareExchange(ref s_defaultJsonEnumConverter, converter, null) ?? converter;
        }

        private static JsonNamingPolicy CreateDefaultJsonNamingPolicy()
        {
            var policy = new JsonSnakeCaseNamingPolicy();
            return Interlocked.CompareExchange(ref s_defaultJsonNamingPolicy, policy, null) ?? policy;
        }

        private static JsonSerializerOptions CreateDefaultJsonSerializerOptions()
        {
            var options = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                Converters = { DefaultJsonEnumConverter },
                PropertyNamingPolicy = DefaultJsonNamingPolicy

            };

            return Interlocked.CompareExchange(ref s_defaultJsonSerializerOptions, options, null) ?? options;
        }


        public static T? As<T>(this JsonElement element)
            => element.Deserialize<T>(DefaultJsonSerializerOptions);

        public static T? As<T>(this JsonElement? element)
            => element is not null ? element.Value.Deserialize<T>(DefaultJsonSerializerOptions) : default;

        public static JsonElement? ToJsonElement(this Object? value)
            => value is not null ?
                 (value is JsonElement j ? j :
                    JsonSerializer.SerializeToElement(value, DefaultJsonSerializerOptions)) : null;

        public static T? FromJsonElement<T>(this JsonElement? value)
            => value.HasValue ?
                 (value is T j ? j :
                    JsonSerializer.Deserialize<T>(value.Value, DefaultJsonSerializerOptions)) : default(T?);

        public static JsonObject? ToJsonObject(this Object? value)
            => value is not null ?
                 (value is JsonObject j ? j :
                    JsonSerializer.SerializeToNode(value, DefaultJsonSerializerOptions)?.AsObject()) : null;

        public static T? FromJsonObject<T>(this JsonObject value)
            => value != null ?
                 (value is T j ? j :
                     JsonSerializer.Deserialize<T>(value, DefaultJsonSerializerOptions)) : default(T?);


        internal static JsonObject ToJsonObjectWithId(this Object? value, int id)
        {
            var o = value?.ToJsonObject();
            if (o is not null)
                o["id"] = id;
            else
            {
                o = new JsonObject();
                o["id"] = id;
            }
            return o;
        }


        public static T? GetAttributeOfEnumMember<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }


    }
}
