using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Colyseus;
using Colyseus.Schema;
using GameDevWare.Serialization;

public class Message<T>
{
  
}

public class NodeMap
{
    public int nodeIndex;
}

public class BountyColyseusManager : ColyseusManager<BountyColyseusManager>
{
    public delegate void OnRoomMessage(string messageType, object message);
    public static event OnRoomMessage onRoomMessage;

    protected ColyseusClient client;
    public ColyseusRoom<LobbySchema> lobbyRoom;
    public ColyseusRoom<GameRoomSchema> gameRoom;

    // Start is called before the first frame update
    private void Start()
    {
        base.InitializeClient();
    }
    public void Connect()
    {
        string endpoint = "wss://dev-game-api.w3w.app";
        client = BountyColyseusManager.Instance.CreateClient(endpoint);
        Debug.Log("Connecting to " + endpoint);
    }

    public async void Disconnect()
    {
        await lobbyRoom.Leave();
    }

    public async void JoinLobby(string _token)
    {
        var options = new Dictionary<string, object>();
        options.Add("authorization", new { token = "Bearer " + _token });
        try
        {
            lobbyRoom = await client.JoinOrCreate<LobbySchema>("lobby", options);
        }catch(Exception e)
        {
            Debug.Log("error");
            Debug.Log(e);
        }
    }

    public async void CreateRoom(string roomType, string _mapKey, string _token)
    {
        Debug.Log("JOIN ROOM");
        var options = new Dictionary<string, object>();
        options.Add("authorization", new { token = "Bearer " + _token });
        options.Add("others", new { mapKey = _mapKey });
        switch (roomType)
        {
            case ROOM_TYPE.GAME_ROOM:
                gameRoom = await client.JoinOrCreate<GameRoomSchema>(ROOM_TYPE.GAME_ROOM, options);
                gameRoom.OnMessage<RollResultMessage>(PLAYER_RECEIVE_EVENTS.ROLL_RESULT, (message) =>
                {
                    onRoomMessage(PLAYER_RECEIVE_EVENTS.ROLL_RESULT, message);
                });
                gameRoom.OnMessage<string>(PLAYER_RECEIVE_EVENTS.FIGHT_RESULT, (message) => {
                    onRoomMessage(PLAYER_RECEIVE_EVENTS.FIGHT_RESULT, message);
                });
                gameRoom.OnMessage<string>(PLAYER_RECEIVE_EVENTS.BATTLE_INIT, (message) => {
                    onRoomMessage(PLAYER_RECEIVE_EVENTS.BATTLE_INIT, message);
                });
                gameRoom.OnMessage<string>(PLAYER_RECEIVE_EVENTS.ERROR, (message) => {
                    onRoomMessage(PLAYER_RECEIVE_EVENTS.ERROR, message);
                });
                break;
            default:
                return;
        }
    }

    public async void Roll  ()
    {
        await gameRoom.Send(PLAYER_SENT_EVENTS.ROLL_DICE);
    }
}
