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
    public delegate void OnRoomMessage(string messageType, object message);
    public static event OnRoomMessage onReceiveMessage;

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
        OnJoinLobbySuccess?.Invoke();
        Debug.Log("[BountyColyseusManager] JoinLobby success.");
    }

    /// <summary>
    /// Assign all room event to gameRoom.
    /// </summary>
    private void AssignRoomEvent()
    {
        gameRoom.OnMessage<RollResultMessage>(PLAYER_RECEIVE_EVENTS.ROLL_RESULT, (message) =>
        {
            onReceiveMessage(PLAYER_RECEIVE_EVENTS.ROLL_RESULT, message);
        });
        gameRoom.OnMessage<string>(PLAYER_RECEIVE_EVENTS.FIGHT_RESULT, (message) => {
            onReceiveMessage(PLAYER_RECEIVE_EVENTS.FIGHT_RESULT, message);
        });
        gameRoom.OnMessage<string>(PLAYER_RECEIVE_EVENTS.BATTLE_INIT, (message) => {
            onReceiveMessage(PLAYER_RECEIVE_EVENTS.BATTLE_INIT, message);
        });
        gameRoom.OnMessage<string>(PLAYER_RECEIVE_EVENTS.ERROR, (message) => {
            onReceiveMessage(PLAYER_RECEIVE_EVENTS.ERROR, message);
        });

        OnJoinRoomSuccess?.Invoke();
    }

    /// <summary>
    /// Trigger the Send data event of the room
    /// </summary>
    /// <param name="_data"></param>
    /// <returns></returns>
    public IEnumerator Send(string _data)
    {
        if(gameRoom.colyseusConnection.IsOpen)
        {
            Task task = gameRoom.Send(_data);
            yield return new WaitUntil(() => task.IsCompleted);
            Debug.Log("[BountyColyseusManager] Send success.");
        }
        else
        {
            Debug.LogError("[BountyColyseusManager] [Send] Room is not available...");
        }
    }
}
