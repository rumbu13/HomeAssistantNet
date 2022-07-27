using HomeAssistantNet.Helpers;
using System.Globalization;
using System.Text;
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

        public static string Prettify(this string str)
        {
            var build = new StringBuilder(str.Length);
            bool nextIsUpper = false;
            bool isFirstCharacter = true;
            foreach (char c in str)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    nextIsUpper = true;
                    continue;
                }

                build.Append(nextIsUpper || isFirstCharacter ? char.ToUpper(c, CultureInfo.InvariantCulture) : c);
                nextIsUpper = false;
                isFirstCharacter = false;
            }

            return build.ToString();
        }
    }
}
