using HomeAssistantNet.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet
{
    public static class HaOptions
    {
        private static readonly Lazy<JsonSerializerOptions> _haDefaultJsonSerializerOptions =
            new(() =>
                new JsonSerializerOptions()
                {
                    WriteIndented = false,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy()
                }, true);

        public static JsonSerializerOptions DefaultJsonSerializerOptions
            => _haDefaultJsonSerializerOptions.Value;       
    }
}
