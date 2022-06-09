using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Colyseus;
using Colyseus.Schema;
using GameDevWare.Serialization;
using System.Collections;

public class BountyColyseusManager : ColyseusManager<BountyColyseusManager>
{
    public delegate void OnRoomMessage<T>(T messageType, object message);
    public OnRoomMessage<GAMEROOM_RECEIVE_EVENTS> onGameReceiveMsg;
    public OnRoomMessage<LOBBY_RECEIVE_EVENTS> onLobbyReceiveMsg;

    public delegate void OnEventTrigger();
    public OnEventTrigger OnJoinLobbySuccess;
    public OnEventTrigger OnJoinRoomSuccess;
    public OnEventTrigger OnDisconected;

    private ColyseusRoom<LobbySchema> lobbyRoom;
    private ColyseusRoom<GameRoomSchema> gameRoom;

    /// <summary>
    /// Start connecting to Socket
    /// </summary>
    /// <param name="endpoint"></param>
    public void Connect(string endpoint)
    {
        client = CreateClient(endpoint);
        Debug.Log("[BountyColyseusManager] Connecting to " + endpoint);
    }

    /// <summary>
    /// Disconnect the socket.
    /// </summary>
    public IEnumerator Disconnect()
    {
        Task task = lobbyRoom.Leave();
        yield return new WaitUntil(() => task.IsCompleted);
        Debug.Log("[BountyColyseusManager] Disconnect...");
        OnDisconected?.Invoke();
    }

    /// <summary>
    /// Create or join a Lobby
    /// </summary>
    /// <param name="_token"></param>
    public IEnumerator JoinLobby(string _token)
    {
        var options = new Dictionary<string, object>
        {
            { "authorization", new { token = "Bearer " + _token } }
        };

        Task<ColyseusRoom<LobbySchema>> task = client.JoinOrCreate<LobbySchema>(ROOM_TYPE.LOBBY_ROOM, options);
        yield return new WaitUntil(() => task.IsCompleted);
        try
        {
            Debug.Log("[BountyColyseusManager] JoinLobby Task IsCompleted.");
            lobbyRoom = task.Result;
            AssignLobbyEvent();
        }
        catch(Exception ex)
        {
            Debug.LogError("[BountyColyseusManager] JoinLobby Error: " + ex.Message);
        }
    }

    /// <summary>
    /// Check the user has been connected to Lobby or not
    /// </summary>
    /// <returns>true = Connected to a lobby</returns>
    public bool LobbyStatus()
    {
        return lobbyRoom != null;
    }
    
    /// <summary>
    /// Create a room with room Type, Map Key and User Token
    /// </summary>
    /// <param name="roomType"></param>
    /// <param name="_mapKey"></param>
    /// <param name="_token">Get this Token from the Login in the Web Client</param>
    /// <returns></returns>
    public IEnumerator CreateRoom(string roomType, string _mapKey, string _token)
    {
        Debug.Log("[BountyColyseusManager] Create and join room.");
        var options = new RoomOption(_token, _mapKey);
        switch (roomType)
        {
            case ROOM_TYPE.GAME_ROOM:
                Task<ColyseusRoom<GameRoomSchema>> task = client.JoinOrCreate<GameRoomSchema>(ROOM_TYPE.GAME_ROOM, options.option);
                yield return new WaitUntil(() => task.IsCompleted);
                Debug.Log("[BountyColyseusManager] CreateRoom success.");
                gameRoom = task.Result;
                //Attach all Multiplayer Event 
                AssignRoomEvent();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Assign all Lobby event to gameRoom
    /// </summary>
    private void AssignLobbyEvent()
    {
        lobbyRoom.OnMessage<string>(LOBBY_RECEIVE_EVENTS.MAP_LIST_RESULT.ToString(), (message) =>
        {
            MapShortList_MSG data = JsonUtility.FromJson<MapShortList_MSG>(message);
            onLobbyReceiveMsg(LOBBY_RECEIVE_EVENTS.MAP_LIST_RESULT, data);
        });
        lobbyRoom.OnMessage<string>(LOBBY_RECEIVE_EVENTS.MAP_NODE_RESULT.ToString(), (message) =>
        {
            Map_MSG data = JsonUtility.FromJson<Map_MSG>(message);
            onLobbyReceiveMsg(LOBBY_RECEIVE_EVENTS.MAP_NODE_RESULT, data);
        });
        OnJoinLobbySuccess?.Invoke();
        Debug.Log("[BountyColyseusManager] AssignLobbyEvent success.");
    }

    /// <summary>
    /// Assign all room event to gameRoom.
    /// </summary>
    private void AssignRoomEvent()
    {
        gameRoom.OnMessage<string>(GAMEROOM_RECEIVE_EVENTS.ROLL_RESULT.ToString(), (message) =>
        {
            onGameReceiveMsg(GAMEROOM_RECEIVE_EVENTS.ROLL_RESULT, message);
        });
        gameRoom.OnMessage<string>(GAMEROOM_RECEIVE_EVENTS.FIGHT_RESULT.ToString(), (message) => {
            onGameReceiveMsg(GAMEROOM_RECEIVE_EVENTS.FIGHT_RESULT, message);
        });
        gameRoom.OnMessage<string>(GAMEROOM_RECEIVE_EVENTS.LUCKY_DRAW_RESULT.ToString(), (message) => {
            onGameReceiveMsg(GAMEROOM_RECEIVE_EVENTS.LUCKY_DRAW_RESULT, message);
        });
        gameRoom.OnMessage<string>(GAMEROOM_RECEIVE_EVENTS.BALANCE_RESULT.ToString(), (message) => {
            onGameReceiveMsg(GAMEROOM_RECEIVE_EVENTS.BALANCE_RESULT, message);
        });

        OnJoinRoomSuccess?.Invoke();
        Debug.Log("[BountyColyseusManager] AssignRoomEvent success.");
    }

    /// <summary>
    /// Trigger the Send data event of the room
    /// </summary>
    /// <param name="_data"></param>
    /// <returns></returns>
    public void Send(SEND_TYPE sendChannel, string _data, object message = null)
    {
        switch (sendChannel)
        {
            case SEND_TYPE.LOBBY_SEND:
                if (lobbyRoom.colyseusConnection.IsOpen)
                {
                    if (message != null)
                        lobbyRoom.Send(_data, message);
                    else
                        lobbyRoom.Send(_data);
                }
                else
                {
                    Debug.LogError("[BountyColyseusManager] [Send] lobbyRoom is not available...");
                }
                break;
            case SEND_TYPE.GAMEROOM_SEND:
                if (gameRoom.colyseusConnection.IsOpen)
                {
                    if(message != null)
                    {
                        Debug.Log("[BountyColyseusManager] Send: "+ sendChannel.ToString() + " | " + _data);
                        gameRoom.Send(_data, message);
                    }                        
                    else
                    {
                        Debug.Log("[BountyColyseusManager] Send no data: " + sendChannel.ToString() + " | " + _data);
                        gameRoom.Send(_data);
                    }                        
                }
                else
                {
                    Debug.LogError("[BountyColyseusManager] [Send] Room is not available...");
                }
                break;
            default:
                Debug.LogError("[BountyColyseusManager] [Send] Channel not supported...");
                break;
        }
        
    }
}
