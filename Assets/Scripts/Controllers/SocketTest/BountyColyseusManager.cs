using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Colyseus;
using Colyseus.Schema;
using GameDevWare.Serialization;

public class Message : Schema
{
    [Colyseus.Schema.Type(0, "int")]
    public int index = 0;

    [Colyseus.Schema.Type(1, "long")]
    public long timestamp = 0;
}

public class BountyColyseusManager : ColyseusManager<BountyColyseusManager>
{
    protected ColyseusClient client;
    protected ColyseusRoom<LobbySchema> lobbyRoom;
    protected ColyseusRoom<GameRoomSchema> gameRoom;

    // Start is called before the first frame update
    public void Connect()
    {
        string endpoint = "ws://localhost:2567";
        //string endpoint = "ws://vps735892.ovh.net:2567";
        client = BountyColyseusManager.Instance.CreateClient(endpoint);
        Debug.Log("Connecting to " + endpoint);
    }

    public async void Disconnect()
    {
        await lobbyRoom.Leave();
    }

    public async void JoinLobby()
    {
        var options = new Dictionary<string, object>();
        var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZGRyZXNzIjoiMHgwMTk4NTU1NTc0NzBlNWIwN2Q2ZmNiMWMwN2Y0ZjJiOGE0YWIwNmYzIiwidXNlcm5hbWUiOiIweDAxOTg1NTU1NzQ3MGU1YjA3ZDZmY2IxYzA3ZjRmMmI4YTRhYjA2ZjMiLCJpYXQiOjE2NTEwMzQxMjYsImV4cCI6MTk2NjYxMDEyNn0.Jrma2FYlhcsRCtNLpXRxkQFm714c73F781pIaNLHfEM";
        options.Add("authorization", new { token = "Bearer " +token });
        lobbyRoom = await client.JoinOrCreate<LobbySchema>("lobby", options);
    }

    public async void JoinRoom(string roomType)
    {
        var options = new Dictionary<string, object>();
        var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZGRyZXNzIjoiMHgwMTk4NTU1NTc0NzBlNWIwN2Q2ZmNiMWMwN2Y0ZjJiOGE0YWIwNmYzIiwidXNlcm5hbWUiOiIweDAxOTg1NTU1NzQ3MGU1YjA3ZDZmY2IxYzA3ZjRmMmI4YTRhYjA2ZjMiLCJpYXQiOjE2NTEwMzQxMjYsImV4cCI6MTk2NjYxMDEyNn0.Jrma2FYlhcsRCtNLpXRxkQFm714c73F781pIaNLHfEM";
        options.Add("authorization", new { token = "Bearer " + token });
        switch (roomType)
        {
            case ROOM_TYPE.GAME_ROOM:
                gameRoom = await client.JoinOrCreate<GameRoomSchema>(ROOM_TYPE.GAME_ROOM, options);
                gameRoom.OnMessage<RollResultMessage>(MESSAGE_TYPE.ROLL_RESULT, (message) => {
                    Debug.Log(message.currentNode);
                });
                gameRoom.OnMessage<RollResultMessage>("join", (message) => {
                    Debug.Log("joined");
                });
                break;
            default:
                return;
        }
    }

    public async void Roll()
    {
        await gameRoom.Send("roll");
    }
}
