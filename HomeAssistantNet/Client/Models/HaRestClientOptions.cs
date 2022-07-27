namespace HomeAssistantNet.Client;

public sealed class HaRestClientOptions
{
    internal HaRestClientOptions() { }

    public TimeSpan SendTimeout { get; set; } = TimeSpan.FromSeconds(5);
    public TimeSpan ReceiveTimeout { get; set; } = TimeSpan.FromSeconds(5);
    public bool UseHttps { get; set; }
    public string? Host { get; set; }
    public int Port { get; set; } = 8123;
    public string? Token { get; set; }

}
