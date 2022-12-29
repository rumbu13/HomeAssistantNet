namespace HomeAssistantNet.Api;

public sealed record HaHomematicImpulseData
{
    public int? Channel { get; init; }
    public string? Name { get; init; }
}
