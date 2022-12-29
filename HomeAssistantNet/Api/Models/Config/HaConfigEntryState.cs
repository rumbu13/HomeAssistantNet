namespace HomeAssistantNet.Api;

public enum HaConfigEntryState
{
    Loaded,
    SetupError,
    MigrationError,
    SetupRetry,
    NotLoaded,
    FailedUnload,
    SetupInProgress
}