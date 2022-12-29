namespace HomeAssistantNet.Core;

public sealed class HaClientOptions
{
    public TimeSpan ConnectTimeout { get; set; } = TimeSpan.FromSeconds(5);
    public TimeSpan SendTimeout { get; set; } = TimeSpan.FromSeconds(5);
    public TimeSpan ReceiveTimeout { get; set; } = TimeSpan.FromSeconds(5);
    public TimeSpan DisconnectTimeout { get; set; } = TimeSpan.FromSeconds(5);
    public TimeSpan ReconnectMinTimeout { get; set; } = TimeSpan.FromSeconds(30);
    public TimeSpan ReconnectMaxTimeout { get; set; } = TimeSpan.FromSeconds(60);
    public bool Secure { get; set; } = false;
    public bool IgnoreCertificateErrors { get; set; } = false;
    public string? Host { get; set; } = "homeassistant";
    public int Port { get; set; } = 8123;
    public string? Token { get; set; }
}
