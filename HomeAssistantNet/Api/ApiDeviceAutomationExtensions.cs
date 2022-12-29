using HomeAssistantNet.Client;
using HomeAssistantNet.Client.Internal;

namespace HomeAssistantNet.Api;

public static class ApiDeviceAutomationExtensions
{
  
    public static Task<HaDeviceAction[]?> GetDeviceActionsAsync(this IHaClient client, string deviceId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaDeviceAction[]>(new
        {
            type = "device_automation/action/list",
            deviceId
        }, cancellationToken);

    public static Task<HaDeviceCondition[]?> GetDeviceConditionsAsync(this IHaClient client, string deviceId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaDeviceCondition[]>(new
        {
            type = "device_automation/condition/list",
            deviceId
        }, cancellationToken);

    public static Task<HaDeviceTrigger[]?> GetDeviceTriggersAsync(this IHaClient client, string deviceId,
        CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaDeviceTrigger[]>(new
        {
            type = "device_automation/trigger/list",
            deviceId
        }, cancellationToken);

    public static Task<HaDeviceCapabilities?> GetDeviceActionCapabilitiesAsync(this IHaClient client, 
        HaDeviceAction action, CancellationToken cancellationToken = default)
        => client.SendCommandAsync<HaDeviceCapabilities>(new
        {
            type = "device_automation/action/capabilities",
            action
        }, cancellationToken);

    public static Task<HaDeviceCapabilities?> GetDeviceConditionCapabilitiesAsync(this IHaClient client,
       HaDeviceCondition condition, CancellationToken cancellationToken = default)
       => client.SendCommandAsync<HaDeviceCapabilities>(new
       {
           type = "device_automation/condition/capabilities",
           condition
       }, cancellationToken);

    public static Task<HaDeviceCapabilities?> GetDeviceTriggerCapabilitiesAsync(this IHaClient client,
      HaDeviceTrigger trigger, CancellationToken cancellationToken = default)
      => client.SendCommandAsync<HaDeviceCapabilities>(new
      {
          type = "device_automation/trigger/capabilities",
          trigger
      }, cancellationToken);

}