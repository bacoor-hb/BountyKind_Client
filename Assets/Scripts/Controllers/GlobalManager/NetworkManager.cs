using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Refill();
    [DllImport("__Internal")]
    private static extern void Login();
    [DllImport("__Internal")]
    private static extern void Logout();

    public delegate void OnEventTrigger<T>(T data);
    public OnEventTrigger<string> OnLoginSuccess;
    public OnEventTrigger<MapShortList_MSG> OnGetMapSuccess;
    public OnEventTrigger<Map_MSG> OnGetMapDetailSuccess;
    public OnEventTrigger<UserData_API> OnGetUserdataSuccess;

    [SerializeField]
    private BountyColyseusManager socketManager;
    [SerializeField]
    private APIManager APIManager;
    private UserDataManager UserDataManager;
    private MapNodeDataManager MapNodeDataManager;

    private string host_EndPoint;
    public void Init()
    {
        UserDataManager = GlobalManager.Instance.UserDataManager;
        MapNodeDataManager = GlobalManager.Instance.MapNodeDataManager;

        APIManager.Init();
        host_EndPoint = CONSTS.HOST_ENDPOINT_SOCKET;

        APIManager.OnGetUserDataFinished = null;
        APIManager.OnGetUserDataFinished += GetUserDataSuccess;
        OnGetMapSuccess = null;
        OnGetMapSuccess += OnGetMapProcess;
        OnGetMapDetailSuccess = null;
        OnGetMapDetailSuccess += OnGetMapDetailProcess;
    }

    #region NETWORK FLOW
    /// <summary>
    /// Join the Game Lobby after connect to socket success.
    /// </summary>
    private void JoinLobby()
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
    /// Connect Socket + Join Lobby
    /// </summary>
    public void Connect()
    {
        socketManager.Connect(host_EndPoint);
        JoinLobby();
    }

    /// <summary>
    /// Disconnect the socket and reload the Login Scene
    /// </summary>
    public void Disconnect()
    {
        socketManager.Disconnect();
        //LoadingManager.LoadWithLoadingScene(SCENE_NAME.MainMenu);
    }
    #endregion

    #region EVENT MANAGEMENT
    public void Refill_External()
    {
        //Call external Refill feature.
        Refill();
    }

    public void Login_External()
    {
        //Call external Login feature.
        Login();
    }

    public void Logout_External()
    {
        //Call external Logout feature.
        Logout();
    }

    public void LoginSuccess(string data)
    {
        OnLoginSuccess?.Invoke(data);
    }

    /// <summary>
    /// Send data to the Game room in the Server
    /// </summary>
    /// <param name="_data"></param>
    public void Send(SEND_TYPE sendChannel, string _data, object message = null)
    {
        if (socketManager != null)
            socketManager.Send(sendChannel, _data, message);
        else
            Debug.LogError("[NetworkManager] Send Error: Socket Manager == null");
    }
    #endregion

    #region Get Data from API
    /// <summary>
    /// Call Get User API
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="_address"></param>
    /// <param name="_token"></param>
    public void GetUserData_FromAPI(string uri, string _token)
    {
        if(UserDataManager.UserData != null)
        {
            StartCoroutine(APIManager.GetUserData(uri, _token));
        }        
    }
    public void GetUserDataSuccess(UserData_API userData)
    {
        UserDataManager.UserData.SetUserData_API(userData);
        OnGetUserdataSuccess?.Invoke(userData);
    }

    /// <summary>
    /// Trigger this event when the lobby return the map List
    /// </summary>
    /// <param name="mapList"></param>
    void OnGetMapProcess(MapShortList_MSG mapList)
    {
        MapNodeDataManager.UpdateMapList(mapList);
    }
    /// <summary>
    /// Trigger this event when the lobby return the map detail
    /// </summary>
    /// <param name="mapList"></param>
    void OnGetMapDetailProcess(Map_MSG map)
    {
        MapNodeDataManager.UpdateMap(map);
    }
    #endregion
}
