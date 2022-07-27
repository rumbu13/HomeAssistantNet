namespace HomeAssistantNet.Client;

public class HaWsConnectingEventArgs : EventArgs
{
    public HaWsConnectingEventArgs(int attempt)
    {
        Attempt = attempt;
    }

    public int Attempt { get; private set; }
}