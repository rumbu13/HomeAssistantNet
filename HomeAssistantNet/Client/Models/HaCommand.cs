namespace HomeAssistantNet.Client;

public class HaCommand
{
    public HaCommand(string type)
    {
        Type = type;
    }

    public int Id { get; internal set; }
    public string Type { get; init; }
}
