namespace HomeAssistantNet.Api;

public sealed record HaDoorbirdEvent : HaStandardEvent
{    
    public HaDoorbirdData? Data { get; init; }
}
