using HomeAssistantNet;
using HomeAssistantNet.Api;
using HomeAssistantNet.Client;
using HomeAssistantNet.Tools;
using System.Text.Json;

namespace HomeAssistant.EventWatcher;

public class EventWorker : BackgroundService
{
    private readonly IHaClient haClient;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2254:Template should be a static expression", Justification = "It's a demo")]
    public EventWorker(IHaClient haClient, ILogger<EventWorker> logger)
    {
        this.haClient = haClient;

        haClient.EventReceived += (sender, args) =>
        {
            switch (args.Event?.)
            {
                case "state_changed":
                    var data = args.Event?.Data;
                    if (data is not null)
                    {
                        var eventData = data.As<HaStateChangeData>();
                        logger.LogInformation($"State change: {eventData?.EntityId} changed from '{eventData?.OldState?.State}' to '{eventData?.NewState?.State}'");
                    }
                    else goto default;
                    break;
                default:
                    logger.LogInformation($"Event received: {args.Event?.EventType}");
                    break;
            }
        };

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested && haClient.IsRunning)
        {
            await Task.Delay(1000, stoppingToken);
        }
    }


}