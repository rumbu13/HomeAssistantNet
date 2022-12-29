namespace HomeAssistantNet.Api;

public sealed record HaEnoceanButtonPressedData
{
    public int? Id { get; init; }
    public int? Pushed { get; init; }
    public int? Which { get; init; }
    public int? Onoff { get; init; }
}
