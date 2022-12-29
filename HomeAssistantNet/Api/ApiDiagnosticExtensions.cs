using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiDiagnosticExtensions
{
    public static Task<HaDiagnosticInfo[]?> GetDiagnosticsAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.GetListAsync<HaDiagnosticInfo>("diagnostics/list", cancellationToken);

    public static Task<HaDiagnosticInfo?> GetDiagnosticAsync(this IHaClient client, string domain,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaDiagnosticInfo>(new { Type = "diagnostics/get", domain }, cancellationToken);



}

