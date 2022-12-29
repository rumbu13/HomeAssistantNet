using HomeAssistantNet.Core;

namespace HomeAssistantNet.Rest;

public static class HaClientOptionsExtensions
{
    public static Uri GetRestUri(this HaClientOptions options)
        => new UriBuilder(options.Secure ? Uri.UriSchemeHttps : Uri.UriSchemeHttp, options.Host, options.Port).Uri;
}
