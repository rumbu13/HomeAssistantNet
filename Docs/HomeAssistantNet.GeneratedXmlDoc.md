# HomeAssistantNet #

## Type Client.HaWsClientOptionsBuilder

 Helper to generate valid options for websocket connection 



---
#### Method Client.HaWsClientOptionsBuilder.Build

 Builds a options object to be used in a websocket connection 

**Returns**: 

[[T:System.ArgumentException|T:System.ArgumentException]]: 

[[T:System.ArgumentOutOfRangeException|T:System.ArgumentOutOfRangeException]]: 



---
## Type Client.HaWsDisconnectedEventArgs

 Disconnected event 



---
#### Method Client.HaWsDisconnectedEventArgs.#ctor(System.Boolean,HomeAssistantNet.Client.HaWsDisconnectReason,System.Exception)

 Creates a new event 

|Name | Description |
|-----|------|
|reconnect: ||
|disconnectReason: ||
|error: ||


---
#### Property Client.HaWsDisconnectedEventArgs.Reconnect

 Set to true to allow reconnection 



---
#### Property Client.HaWsDisconnectedEventArgs.DisconnectReason

 Reason fror disconnection 



---
#### Property Client.HaWsDisconnectedEventArgs.Error

 Error leading to disconnection 



---
## Type Client.HaWsDisconnectReason

 Disconnect reason when connection is lost or failed 



---
#### Field Client.HaWsDisconnectReason.AuthenticationFailed

 Authentication failed 



---
#### Field Client.HaWsDisconnectReason.UserInitiated

 User cancelled connection 



---
#### Field Client.HaWsDisconnectReason.Error

 Unknown error was received 



---
#### Field Client.HaWsDisconnectReason.Timeout

 Timeout was encountered while sending, receiving or connecting 



---
#### Field Client.HaWsDisconnectReason.ConnectionClosed

 Server closed current connection 



---
#### Field Client.HaWsDisconnectReason.InvalidData

 Invalid data was received 



---
## Type Client.HaWsError

 JSON format error sent by Home Assistant through WebSocket 



---


