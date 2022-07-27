namespace HomeAssistantNet.Client;

public sealed class HaConnectedEventArgs : EventArgs
{
    public HaConnectedEventArgs(string? version)
    {
        Version = version;
    }

    public string? Version { get; private set; }
}