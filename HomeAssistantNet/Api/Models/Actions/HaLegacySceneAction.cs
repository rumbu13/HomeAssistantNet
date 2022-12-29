using System.Text.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public sealed record HaLegacySceneAction : HaAction
{    
    public string? Scene { get; init; }
}
