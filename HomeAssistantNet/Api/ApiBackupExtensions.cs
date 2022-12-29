using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiBackupExtensions
{
  
    public static Task<HaBackupInfo?> GetBackupInfoAsync(this IHaClient client,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaBackupInfo>(new { type = "backup/info" }, cancellationToken);

    public static Task<HaBackup?> CreateBackupAsync(this IHaClient client, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaBackup>(new { type = "backup/generate" }, cancellationToken);


  
}