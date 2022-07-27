using HomeAssistantNet.Client;

namespace HomeAssistantNet.Client.Internal;

internal sealed record HaUnsubcribeEvents : HaWsCommand
{
    public HaUnsubcribeEvents(int subscription)
        : base("unsubscribe_events")
    {
        Subscription = subscription;
    }
    public int Subscription { get; init; }
}
