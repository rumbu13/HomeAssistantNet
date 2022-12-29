namespace HomeAssistantNet.Api;

public sealed record HaTtsInfo
{
    public string[][]? Languages { get; init; }
}
