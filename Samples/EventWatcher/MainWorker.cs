using HomeAssistantNet.Client;
using Microsoft.Extensions.Options;

namespace HomeAssistant.EventWatcher;

public class MainWorker : BackgroundService
{
    private readonly ILogger<MainWorker> logger;
    private readonly IHaClient haClient;
    private readonly HaClientOptions options;
    private readonly DateTime StartedOn = DateTime.UtcNow;
    private long messageCount;

    public MainWorker(ILogger<MainWorker> logger, IHaClient haWsClient, IOptions<HaClientOptions> options)
    {
        this.logger = logger;
        this.haClient = haWsClient;
        this.options = options.Value;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {



        haClient.Connected += (sender, args) =>
        {
            logger.LogInformation("Connected to {host} {port}, version {version}",
            options.Host, options.Port, args.Version);
        };

        haClient.Disconnected += (sender, args) =>
        {
            logger.LogWarning("Disconnected, reason: {reason}, {exception}", args.DisconnectReason, args.Error?.Message);
            if (!args.Reconnect)
                logger.LogError("Cannot reconnect, service will be stopped.");
        };

        haClient.Connecting += (sender, args)
            => logger.LogWarning("Connecting, attempt no. {attempt}", args.Attempt);

        haClient.EventReceived += (sender, args)
            => Interlocked.Increment(ref messageCount);

        await haClient.StartAsync(options);

        await base.StartAsync(cancellationToken);

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested && haClient.IsRunning)
        {
            logger.LogInformation("System uptime is {uptime}, {msgs} messages processed", DateTime.UtcNow - StartedOn, messageCount);
            await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (haClient.IsRunning)
            await haClient.StopAsync();
        await base.StopAsync(cancellationToken);
    }
}