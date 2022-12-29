namespace HomeAssistantNet.Api;

public sealed record HaHomeAssistantTrigger : HaTrigger
{    
    public HaHomeAssistantEvent Event { get; init; }

}
