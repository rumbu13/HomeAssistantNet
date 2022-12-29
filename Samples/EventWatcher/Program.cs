using HomeAssistant.EventWatcher;
using HomeAssistantNet.Client;
using HomeAssistantNet.Tools;
using Microsoft.Extensions.Logging;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<MainWorker>();
        services.AddHostedService<EventWorker>();
        services.Configure<HaClientOptions>(context.Configuration.GetSection("HomeAssistant"));

    })
    .UseHomeAsistantClient() 
    .Build();

await host.RunAsync();
