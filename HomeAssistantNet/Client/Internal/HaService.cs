using HomeAssistantNet.Client;

namespace HomeAssistantNet.Client.Internal;

internal sealed class HaServiceCall : HaCommand
{
    public HaServiceCall(string domain, string service, IEnumerable<string>? targets, object? serviceData)
        : base("call_service")
    {
        Domain = domain;
        Service = service;
        Target = targets?.ToArray();
    }
    public string Domain { get; init; }
    public string Service { get; init; }
    public string[]? Target { get; init; }
    public object? ServiceData { get; init; }

}




