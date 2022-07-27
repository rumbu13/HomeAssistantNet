using HomeAssistantNet.Client;

namespace HomeAssistantNet.Client.Internal;

internal sealed record HaConfigEntryList : HaWsCommand
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



