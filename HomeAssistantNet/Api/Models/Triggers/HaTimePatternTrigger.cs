namespace HomeAssistantNet.Api;

public sealed record HaTimePatternTrigger: HaTrigger
{
    public double? Hours { get; init; }
    public double? Minutes { get; init; }
    public double? Seconds { get; init; }
}
