namespace HomeAssistantNet.Api;

public sealed record HaCallServiceEvent : HaStandardEvent
{
    public HaCallServiceData? Data { get; init; }
}
