using HomeAssistantNet.Client;

namespace HomeAssistantNet.Client;

public record HaWsCommand
{
    public HaWsCommand(string type)
    {
        Type = type;
    }

    public int Id { get; internal set; }
    public string Type { get; init; }
}
