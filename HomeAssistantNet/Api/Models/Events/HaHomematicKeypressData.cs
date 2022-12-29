namespace HomeAssistantNet.Api;

public sealed record HaHomematicKeypressData
{
    public int? Channel { get; init; }
    public string? Name { get; init; }
    public string? Param { get; init; }
}
