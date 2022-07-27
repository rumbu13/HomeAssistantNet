# HomeAssistantNet #

#### Method Api.ApiCoreExtensions.PingAsync(HomeAssistantNet.Client.IHaClient,System.Threading.CancellationToken)

 Sends a "ping" command expecting a "pong" response. 

|Name | Description |
|-----|------|
|client: |The [[|T:HomeAssistantNet.Client.IHaClient]] used to send the command|
|cancellationToken: |The [[|T:System.Threading.CancellationToken]] to monitor for a cancellation request|


---
#### Method Api.ApiCoreExtensions.GetConfigAsync(HomeAssistantNet.Client.IHaClient,System.Threading.CancellationToken)

 Retrieves the current Home Assistant configuration. 

|Name | Description |
|-----|------|
|client: |The [[|T:HomeAssistantNet.Client.IHaClient]] used to send the command|
|cancellationToken: |The [[|T:System.Threading.CancellationToken]] to monitor for a cancellation request|
**Returns**: A [[|T:HomeAssistantNet.Api.HaConfig]] record representing the current Home Assistant configuration



---
## Type Client.HaClientOptionsBuilder

 Helper to generate valid options for websocket connection 



---
#### Method Client.HaClientOptionsBuilder.Build

 Builds a options object to be used in a websocket connection 

**Returns**: 

[[T:System.ArgumentException|T:System.ArgumentException]]: 

[[T:System.ArgumentOutOfRangeException|T:System.ArgumentOutOfRangeException]]: 



---


