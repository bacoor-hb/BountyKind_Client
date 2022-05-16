using UnityEngine;
using UnityEngine.UI;
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
    }

    /// <summary>
    /// Create or join a Lobby
    /// </summary>
    /// <param name="_token"></param>
    public IEnumerator JoinLobby(string _token)
    {
        var options = new Dictionary<string, object>();
        options.Add("authorization", new { token = "Bearer " + _token });
        
        Task<ColyseusRoom<LobbySchema>> task = client.JoinOrCreate<LobbySchema>(ROOM_TYPE.LOBBY_ROOM, options);
        yield return new WaitUntil(() => task.IsCompleted);
        Debug.Log("[BountyColyseusManager] JoinLobby success.");
        lobbyRoom = task.Result;
        AssignLobbyEvent();        
    }

    /// <summary>
    /// Check the user has been connected to Lobby or not
    /// </summary>
    /// <returns>true = Connected to a lobby</returns>
    public bool LobbyStatus()
    {
        return lobbyRoom != null;
    }
    public IEnumerator CreateRoom(string roomType, string _mapKey, string _token)
    {
        Debug.Log("[BountyColyseusManager] Create and join room.");
        var options = new Dictionary<string, object>();
        options.Add("authorization", new { token = "Bearer " + _token });
        options.Add("others", new { mapKey = _mapKey });
        switch (roomType)
        {
            case ROOM_TYPE.GAME_ROOM:
                Task<ColyseusRoom<GameRoomSchema>> task = client.JoinOrCreate<GameRoomSchema>(ROOM_TYPE.GAME_ROOM, options);
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
    }


    public IEnumerator Send(string _data)
    {
        Task task = gameRoom.Send(_data);
        yield return new WaitUntil(() => task.IsCompleted);
        Debug.Log("[BountyColyseusManager] Send success.");
    }
}
