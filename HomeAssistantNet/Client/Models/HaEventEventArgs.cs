namespace HomeAssistantNet.Client;

public sealed class HaEventEventArgs : EventArgs
{
    internal HaEventEventArgs(HaEvent @event)
    {
        Event = @event;
    }

    public HaEvent Event { get; internal set; }

}