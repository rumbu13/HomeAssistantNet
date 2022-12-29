namespace HomeAssistantNet.Api;

public sealed record HaIssy994ControlEvent : HaStandardEvent
{
    public HaIssy994ControlData? Data { get; init; }
}
