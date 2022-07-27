using HomeAssistantNet.Api;
using HomeAssistantNet.Client;
using Microsoft.Extensions.Options;

namespace HomeAssistant.EventWatcher;

public class MainWorker : BackgroundService
{
    private readonly ILogger<MainWorker> _logger;
    private readonly IHaClient _haWsClient;
    private readonly HaOptions _options;
    private readonly DateTime StartedOn = DateTime.UtcNow;
    private long messageCount;

    public MainWorker(ILogger<MainWorker> logger, IHaClient haWsClient, IOptions<HaOptions> options)
    {
        _logger = logger;
        _haWsClient = haWsClient;
        _options = options.Value;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {

        var clientOptions = new HaClientOptionsBuilder()
            .WithHost(_options!.Host!)
            .WithPort(_options!.Port ?? 8123)
            .WithConnectTimeout(TimeSpan.FromSeconds(30))
            .WithTimeout(TimeSpan.FromSeconds(30))
            .WithReconnectMinTimeout(TimeSpan.FromSeconds(1))
            .WithReconnectMaxTimeout(TimeSpan.FromSeconds(60))
            .WithToken(_options!.Token!).Build();

      
        _haWsClient.Connected += (sender, args) =>
        {
            _logger.LogInformation("Connected to {host} {port}, version {version}",
            clientOptions!.Host, clientOptions.Port, args.Version);
        };

        _haWsClient.Disconnected += (sender, args) =>
        {
            _logger.LogWarning("Disconnected, reason: {reason}, {exception}", args.DisconnectReason, args.Error?.Message);
            if (!args.Reconnect)
                _logger.LogError("Cannot reconnect, service will be stopped.");
        };

        _haWsClient.Connecting += (sender, args)
            => _logger.LogWarning("Connecting, attempt no. {attempt}", args.Attempt);

        _haWsClient.EventReceived += (sender, args)
            => Interlocked.Increment(ref messageCount);

        _haWsClient.Start(clientOptions);

        await base.StartAsync(cancellationToken);

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested && _haWsClient.IsRunning)
        {
            _logger.LogInformation("System uptime is {uptime}, {msgs} messages processed", DateTime.UtcNow - StartedOn, messageCount);
            await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_haWsClient.IsRunning)
            _haWsClient.Stop();
        await base.StopAsync(cancellationToken);
    }
}