namespace HomeAssistantNet.Client.Internal;

internal sealed class HaConfigEntryList : HaCommand
{
    public HaConfigEntryList(string? typeFilter, string? domain)
        : base("config_entries/get")
    {
        TypeFilter = typeFilter;
        Domain = domain;
    }
    public string? TypeFilter { get; init; }
    public string? Domain { get; init; }
}



