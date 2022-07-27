namespace HomeAssistantNet.Client.Internal;

internal sealed class HaMessageEventArgs : EventArgs
{
    internal HaMessageEventArgs() { }

    public HaMessageEventArgs(HaMessage? message)
    {
        Message = message;
    }

    public HaMessage? Message { get; internal set; }


}