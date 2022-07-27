namespace HomeAssistantNet.Client.Internal;

internal sealed class HaUnsubcribeEvents : HaCommand
{
    public HaUnsubcribeEvents(int subscription)
        : base("unsubscribe_events")
    {
        Subscription = subscription;
    }
    public int Subscription { get; init; }
}
