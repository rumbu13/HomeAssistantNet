using HomeAssistantNet;
using HomeAssistantNet.Api;
using HomeAssistantNet.Client;
using HomeAssistantNet.Tools;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace HomeAssistant.EventWatcher;

public class EventWorker : BackgroundService
{
    private readonly IHaClient haClient;
    private int subscriptionId;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2254:Template should be a static expression", Justification = "It's a demo")]
    public EventWorker(IHaClient haClient, ILogger<EventWorker> logger)
    {
        this.haClient = haClient;

        haClient.EventReceived += (sender, args) =>
        {
            if (args.Event is HaStateChangedEvent sc)
                logger.LogInformation($"State change: {sc.Data?.EntityId} changed from '{sc?.Data?.OldState?.State}' to '{sc?.Data?.NewState?.State}'");
            else if (args.Event is HaStandardEvent se)
                logger.LogInformation($"Standard event: {se.EventType}");
            else
                logger.LogInformation($"Other event: {args.Event.GetType()}");
        };
    }


    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        subscriptionId = await haClient.SubscribeToEventsAsync(null, cancellationToken);
        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested && haClient.IsRunning)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (haClient.IsConnected)
        {
            await haClient.UnsubscribeAsync(subscriptionId, cancellationToken);
        }
        await base.StopAsync(cancellationToken);
    }


}