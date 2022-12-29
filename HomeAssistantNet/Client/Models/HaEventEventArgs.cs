using HomeAssistantNet.Api;

namespace HomeAssistantNet.Client;

public sealed class HaEventEventArgs : EventArgs
{
    internal HaEventEventArgs(HaEvent @event, int subscriptionId)
    {
        Event = @event;
        SubscriptionId = subscriptionId;
    }

    public int SubscriptionId { get; private set;}
    public HaEvent Event { get; internal set; }

}