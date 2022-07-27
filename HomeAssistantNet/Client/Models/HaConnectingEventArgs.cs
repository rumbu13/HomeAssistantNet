namespace HomeAssistantNet.Client;

public sealed class HaConnectingEventArgs : EventArgs
{
    public HaConnectingEventArgs(int attempt)
    {
        Attempt = attempt;
    }

    public int Attempt { get; private set; }
}