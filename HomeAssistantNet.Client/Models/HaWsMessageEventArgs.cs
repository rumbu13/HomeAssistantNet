namespace HomeAssistantNet.Client;

public class HaWsMessageEventArgs : EventArgs
{
    internal HaWsMessageEventArgs() { }

    public HaWsMessageEventArgs(HaWsMessage? message)
    {
        Message = message;
    }

    public HaWsMessage? Message { get; internal set; }


}