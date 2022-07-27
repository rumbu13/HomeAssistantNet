namespace HomeAssistantNet.Api;

public sealed record HaColorTempSelector
{
    public int? MaxMireds { get; init; }
    public int? MinMireds { get; init; }
}