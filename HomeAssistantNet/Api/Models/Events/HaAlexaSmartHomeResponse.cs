namespace HomeAssistantNet.Api
{
    public sealed record HaAlexaSmartHomeResponse
    {
        public string? Namespace { get; init; }
        public string? Name { get; init; }
    }
}