namespace HomeAssistantNet.Api;

public sealed record HaPersonList
{
    public HaPerson[]? Storage { get; init; }
    public HaPerson[]? Config { get; init; }
}
