using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiAuthExtensions
{
    public static Task<HaUser?> GetCurrentUserAsync(this IHaClient client, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaUser>(new { type = "auth/current_user" }, cancellationToken);

    public static Task<string?> CreateAccessTokenAsync(this IHaClient client, int lifespan, string clientName, 
        string? clientIcon = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<string>(new { type = "auth/long_lived_access_token", lifespan, clientName, clientIcon }, cancellationToken);

    public static Task<HaAuthToken[]?> GetAccessTokensAsync(this IHaClient client, 
        CancellationToken cancellationToken = default)
        => client.GetListAsync<HaAuthToken>("auth/refresh_tokens", cancellationToken);

    public static Task DeleteAccessTokenAsync(this IHaClient client, string refreshTokenId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<Object>(new { type = "auth/delete_refresh_token", refreshTokenId }, cancellationToken);

    public static Task<HaSignedPath?> SignPathAsync(this IHaClient client, string path, int? expires = default,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaSignedPath>(new { type = "auth/sign_path", path, expires }, cancellationToken);

    public static Task<HaUser[]?> GetUsersAsync(this IHaClient client, string path, int? expires = default,
        CancellationToken cancellationToken = default)
        => client.GetListAsync<HaUser>("config/auth/list", cancellationToken);

    public static Task DeleteUserAsync(this IHaClient client, string userId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new 
        { 
            type = "config/auth/delete",
            userId,
        }, cancellationToken);

    public static Task<HaUser?> CreateUserAsync(this IHaClient client, string name, IEnumerable<string>? groupIds = default,
        bool? localOnly = default, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaUser>(new
        {
            type = "config/auth/create",
            name,
            groupIds = groupIds?.ToArray(),
            localOnly,
        }, cancellationToken);

    public static Task<HaUser?> UpdateUserAsync(this IHaClient client, string userId, string? name = default,
        bool? isActive = default, IEnumerable<string>? groupIds = default, bool? localOnly = default, 
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaUser>(new
        {
            type = "config/auth/update",
            userId,
            name,
            isActive,
            groupIds = groupIds?.ToArray(),
            localOnly,
        }, cancellationToken);

    public static Task CreateCredentialsAsync(this IHaClient client, string userId, string username, string password,  
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "config/auth_provider/homeassistant/create",
            userId,
            username,
            password
        }, cancellationToken);

    public static Task DeleteCredentialsAsync(this IHaClient client, string userId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "config/auth_provider/homeassistant/delete",
            userId
        }, cancellationToken);

    public static Task ChangePasswordAsync(this IHaClient client, string currentPassword, string newPassword,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "config/auth_provider/homeassistant/change_password",
            currentPassword,
            newPassword
        }, cancellationToken);

    public static Task ChangeUserPasswordAsync(this IHaClient client, string userId, string password,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<object>(new
        {
            type = "config/auth_provider/homeassistant/admin_change_password",
            userId,
            password
        }, cancellationToken);
}