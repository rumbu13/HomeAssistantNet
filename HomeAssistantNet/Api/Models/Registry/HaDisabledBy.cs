using HomeAssistantNet.Json;
using System.Text.Json.Serialization;

namespace HomeAssistantNet.Api;

public enum HaDisabledBy
{
    User,
    Integration,
    ConfigEntry
}
