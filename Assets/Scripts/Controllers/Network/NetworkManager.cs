using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

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

    public BountyColyseusManager GetSocketManager()
    {
        return socketManager;
    }

    #region NETWORK FLOW
    /// <summary>
    /// Join the Game Lobby after connect to socket success.
    /// </summary>
    public void JoinLobby()
    {
        if(!socketManager.LobbyStatus())
            StartCoroutine(socketManager.JoinLobby(UserDataManager.UserData.token));
    }
    /// <summary>
    /// Create the room with map key and room type.
    /// </summary>
    /// <param name="roomType"></param>
    /// <param name="mapKey"></param>
    public void CreateRoom(string roomType, string mapKey)
    {
        if(socketManager != null)
        {
            StartCoroutine(socketManager.CreateRoom(roomType, mapKey, UserDataManager.UserData.token));
        }
        else
        {
            Debug.LogError("[NetworkManager] Socket is null, cannot create room.");
        }
        
    }
    /// <summary>
    /// Disconnect the socket and reload the Login Scene
    /// </summary>
    public void ExitGame()
    {
        StartCoroutine(socketManager.Disconnect());
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
    #endregion

    #region ROOM EVENT
    public void Refill_External()
    {
        //Call external Refill feature.
        Refill();
    }

    /// <summary>
    /// Send data to the Game room in the Server
    /// </summary>
    /// <param name="_data"></param>
    public void Send(string _data)
    {
        if (socketManager != null)
            StartCoroutine(socketManager.Send(_data));
        else
            Debug.LogError("[NetworkManager] Send Error: Socket Manager == null");
    }

    #endregion
}
