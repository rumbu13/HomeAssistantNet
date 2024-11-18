using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HomeAssistantNet.Tools;

public static class DependencyInjection
{
    public static IHostBuilder UseHomeAsistantClient(this IHostBuilder builder)
        => builder.ConfigureServices((c, s) => s.AddHomeAssistantClient());


    public static IServiceCollection AddHomeAssistantClient(this IServiceCollection collection)
        => collection.AddSingleton<IHaClient, HaClient>();

}

