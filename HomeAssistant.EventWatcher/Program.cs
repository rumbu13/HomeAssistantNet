using HomeAssistant.EventWatcher;
using HomeAssistant.EventWatcher.Automations;
using HomeAssistantNet.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {

        services.AddSingleton<IHaWsClient, HaWsClient>();
        services.AddSingleton<IHaRestClient, HaRestClient>();


        services.AddHostedService<MainWorker>();
        services.AddHostedService<SecondaryService>();

        

        services.Configure<HaOptions>(context.Configuration.GetSection("HomeAssistant"));

    })
    .Build();

await host.RunAsync();
