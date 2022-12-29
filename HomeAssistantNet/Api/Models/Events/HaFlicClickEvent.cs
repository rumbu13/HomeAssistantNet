namespace HomeAssistantNet.Api;

public sealed record HaFlicClickEvent : HaStandardEvent
{    
    public HaFlicClickData? Data { get; init; }
}
