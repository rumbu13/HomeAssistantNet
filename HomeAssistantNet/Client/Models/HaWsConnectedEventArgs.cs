namespace HomeAssistantNet.Client;

public class HaWsConnectedEventArgs : EventArgs
{
    public HaWsConnectedEventArgs(string? version)
    {
        Version = version;
    }

    public string? Version { get; private set; }
}