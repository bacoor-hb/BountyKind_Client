using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Refill();
   
    [SerializeField]
    private BountyColyseusManager socketManager;

    private UserDataManager UserDataManager;
    private LoadingManager LoadingManager;

    private string host_EndPoint;
    public void Init()
    {
        UserDataManager = GlobalManager.Instance.UserDataManager;
        LoadingManager = GlobalManager.Instance.LoadingManager;

        host_EndPoint = CONSTS.HOST_ENDPOINT_DEFAULT;
    }

    /// <summary>
    /// Update User Data
    /// </summary>
    /// <param name="_user"></param>
    public void SetUser(UserData _user)
    {
        UserDataManager.SetUserData(_user);
    }

    public void SetRollNumber(int _rollNumber)
    {
        UserDataManager.UserData.rollNumber = _rollNumber;
    }

    public BountyColyseusManager GetSocketManager()
    {
        return socketManager;
    }

    

    public void JoinLobby()
    {
        if(socketManager.lobbyRoom == null)
            socketManager.JoinLobby(this.UserDataManager.UserData.token);
    }

    public void CreateRoom(string roomType, string mapKey)
    {
        socketManager.CreateRoom(roomType, mapKey, this.UserDataManager.UserData.token);
    }
    public async void Roll()
    {
        if (this.UserDataManager.UserData.rollNumber > 0)
        {
            await socketManager.gameRoom.Send(PLAYER_SENT_EVENTS.ROLL_DICE);
            UserDataManager.UserData.rollNumber--;
        }
        else
        {
            Refill();
        }
    }

    public void ExitGame()
    {
        LoadingManager.LoadWithLoadingScene(SCENE_NAME.Test_Login_Success);
    }
    public void Connect()
    {
        socketManager.Connect(host_EndPoint);
    }

    public void Disconnect()
    {
        socketManager.Disconnect();
    }
}
