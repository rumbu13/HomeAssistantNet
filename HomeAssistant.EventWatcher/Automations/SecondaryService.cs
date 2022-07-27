using HomeAssistantNet.Api;
using HomeAssistantNet.Client;

using Microsoft.Extensions.Options;

namespace HomeAssistant.EventWatcher.Automations;

public class SecondaryService : BackgroundService
{
    private readonly ILogger<SecondaryService> logger;

    public SecondaryService(ILogger<SecondaryService> logger)
    {
        this.logger = logger;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {


        await base.StartAsync(cancellationToken);

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {

        await base.StopAsync(cancellationToken);
    }
}