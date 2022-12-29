namespace HomeAssistantNet.Api;

public sealed record HaSunTrigger : HaTrigger
{
    public double Offset { get; init; }
    public HaSunEvent Event { get; init; }
}
