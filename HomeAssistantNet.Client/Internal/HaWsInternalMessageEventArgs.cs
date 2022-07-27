namespace HomeAssistantNet.Client.Internal;

internal class HaWsInternalMessageEventArgs : HaWsMessageEventArgs
{
    public HaWsInternalMessageEventArgs(HaWsMessage? message)
        :base(message) { }

    public bool Dispatch { get; set; } = true;
}