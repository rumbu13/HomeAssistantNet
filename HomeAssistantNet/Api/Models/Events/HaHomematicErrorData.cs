namespace HomeAssistantNet.Api;

public sealed record HaHomematicErrorData
{
    public int? Error { get; init; }
    public string? Message { get; init; }
}
