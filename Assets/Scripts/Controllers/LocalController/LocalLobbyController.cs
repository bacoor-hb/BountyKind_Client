using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalLobbyController : LocalSingleton<LocalLobbyController>
{
    [SerializeField]
    private LobbyView LobbyView;
    private FormationViewManager FormationViewManager;

    private List<MapShort_MSG> bountyMaps;

    private UserDataManager UserDataManager;
    private MapNodeDataManager MapNodeDataManager;
    private NetworkManager NetworkManager;
    private LoadingManager LoadingManager;
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager = GlobalManager.Instance.NetworkManager;
        BountyColyseusManager.Instance.OnJoinLobbySuccess = null;
        BountyColyseusManager.Instance.OnJoinLobbySuccess += OnConnectSuccess;

        BountyColyseusManager.Instance.OnJoinRoomSuccess = null;
        BountyColyseusManager.Instance.OnJoinRoomSuccess += OnCreateRoomSuccess;

        BountyColyseusManager.Instance.onLobbyReceiveMsg = null;
        BountyColyseusManager.Instance.onLobbyReceiveMsg += HandleLobbyMessage;
        NetworkManager.OnGetUserdataSuccess = null;
        NetworkManager.OnGetUserdataSuccess += OnGetUserDataAPI_Sucess;
        NetworkManager.OnLoginSuccess = null;
        NetworkManager.OnLoginSuccess += LoginSuccess;

        UserDataManager = GlobalManager.Instance.UserDataManager;
        MapNodeDataManager = GlobalManager.Instance.MapNodeDataManager;
        LoadingManager = GlobalManager.Instance.LoadingManager;

        //Lobby View Setting
        InitLobbyView();

        //Formation init
        FormationController.Instance.Init();
        FormationViewManager = FormationController.Instance.viewManager;
        FormationViewManager.Init();
        FormationViewManager.SetFormationCanvasState(false);
        FormationViewManager.SetFormationViewState(FORMATION_VIEW_STYLE.LOBBY);

        FormationController.Instance.OnBackButtonTrigger = null;
        FormationController.Instance.OnBackButtonTrigger += HandleBackLobbyView;
    }

    /// <summary>
    /// Initialize the Lobby View
    /// </summary>
    void InitLobbyView()
    {
        LobbyView.Init();

        LobbyView.CreateRoom_Btn.enabled = false;
        LobbyView.CreateRoom_Btn.onClick.RemoveAllListeners();
        LobbyView.CreateRoom_Btn.onClick.AddListener(CreateRoom);

        LobbyView.Login_Btn.onClick.AddListener(() => { HandleLogin(); });
        LobbyView.Logout_Btn.onClick.AddListener(() => { HandleLogout(); });

        LobbyView.Formation_Btn.onClick.AddListener(() => { HandleOpenFormation(); });
    }

    #region LOGIN Event
    /// <summary>
    /// Call External Login feature. The data will be faked in UNITY_EDITOR
    /// </summary>
    public void HandleLogin()
    {
#if UNITY_EDITOR
        UserData user = new UserData();
        user.GenerateFakeData();
        NextLoginStep(user);
#else
        NetworkManager.Login_External();
#endif
    }

    /// <summary>
    /// Call External Logout.
    /// </summary>
    public void HandleLogout()
    {
        NetworkManager.Logout_External();
    }

    /// <summary>
    /// Called by Web Client when Login Success
    /// </summary>
    /// <param name="data"></param>
    public void LoginSuccess(string data)
    {
        UserData user = new UserData();
        user.SetUserData_FromWeb(JsonUtility.FromJson<UserData_WebLogin>(data));
        NextLoginStep(user);
    }

    private void NextLoginStep(UserData _dataReceived)
    {
        UserDataManager.SetUserData(_dataReceived);

        NetworkManager.Connect();
    }

    /// <summary>
    /// Trigger when the user join lobby success
    /// </summary>
    void OnConnectSuccess()
    {
        Debug.Log("[RoomEventController] GetUserData_FromServer");
        NetworkManager.Send(SEND_TYPE.LOBBY_SEND, LOBBY_SENT_EVENTS.MAP_LIST.ToString());
        NetworkManager.GetUserData_FromAPI(CONSTS.HOST_GET_USERDATA_API, UserDataManager.UserData.token);
    }
    #endregion

    #region ROOM Event
    /// <summary>
    /// Trigger the Create room.
    /// </summary>
    void CreateRoom()
    {
        if (bountyMaps != null && bountyMaps.Count > 0)
        {

            NetworkManager.CreateRoom(ROOM_TYPE.GAME_ROOM, UserDataManager.GetCurrentMapId());
        }
        else
            Debug.LogError("[RoomEventController] Create Room ERROR: No Map Data.");
    }

    /// <summary>
    /// Trigger When a room is created
    /// </summary>
    void OnCreateRoomSuccess()
    {
        //Create Loading map Component.
        Loading_LoadMap loading_LoadMap = LoadingManager.gameObject.AddComponent<Loading_LoadMap>();
        loading_LoadMap.Init(CONSTS.LOADING_DETAIL_LOADMAP, 0.5f, UserDataManager.GetCurrentMapId());
        LoadingManager.AddLoadingAction(loading_LoadMap);

        //Call the Load Scene via the Loading Scene.
        LoadingManager.LoadWithLoadingScene(SCENE_NAME.GameScene);
    }
    #endregion

    #region Handle Event from Server
    void HandleLobbyMessage(LOBBY_RECEIVE_EVENTS messageType, object message)
    {
        try
        {
            switch (messageType)
            {
                case LOBBY_RECEIVE_EVENTS.MAP_LIST_RESULT:
                    Debug.Log("[HandleLobbyMessage] MAP LIST RESULT.");
                    //Convert Server Data to Game Data
                    MapShortList_MSG mapShortList = (MapShortList_MSG)message;
                    NetworkManager.OnGetMapSuccess?.Invoke(mapShortList);
                    List<MapShort_MSG> mapShortLst = new List<MapShort_MSG>();
                    for (int i = 0; i < mapShortList.maps.Length; i++)
                    {
                        mapShortLst.Add(new MapShort_MSG(mapShortList.maps[i].key, mapShortList.maps[i].name, mapShortList.maps[i].totalNode));
                    }
                    OnGetMapSuccess(mapShortLst);
                    break;
                case LOBBY_RECEIVE_EVENTS.MAP_NODE_RESULT:
                    Debug.Log("[HandleLobbyMessage] MAP NODE RESULT.");
                    Map_MSG mapSchema = (Map_MSG)message;
                    Debug.Log(mapSchema.name);
                    NetworkManager.OnGetMapDetailSuccess?.Invoke(mapSchema);
                    break;
                default:
                    Debug.Log("[HandleLobbyMessage] Default");
                    break;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("[HandleLobbyMessage] ERROR:" + ex.Message);
        }
    }
    /// <summary>
    /// Trigger when the server return the map list: Update the map list on the UI.
    /// </summary>
    /// <param name="mapList"></param>
    void OnGetMapSuccess(List<MapShort_MSG> mapList)
    {
        bountyMaps = mapList;

        LobbyView.SetLoobyFormStatus(true);
        LobbyView.SetLoginFormStatus(false);

        LobbyView.CreateRoom_Btn.enabled = true;
        LobbyView.UpdateMapList(mapList);

        UserDataManager.SetCurrentMap(bountyMaps[0].key);
        LobbyView.RoomType_DD.onValueChanged.RemoveAllListeners();
        LobbyView.RoomType_DD.onValueChanged.AddListener((currentMap) =>
        {
            UserDataManager.SetCurrentMap(bountyMaps[currentMap].key);
        });


    }

    /// <summary>
    /// Trigger when the Server return the user Data.
    /// </summary>
    /// <param name="userData"></param>
    void OnGetUserDataAPI_Sucess(UserData_API userData)
    {
        LobbyView.FillData(UserDataManager.UserData);
    }
    #endregion
    void HandleOpenFormation()
    {
        Debug.Log("HandleOpenFormation");
        FormationController.Instance.viewManager.SetFormationCanvasState(true);
        FormationController.Instance.GetUserFormationData();
        LobbyView.SetLobbyViewState(false);
    }

    void HandleBackLobbyView()
    {
        FormationController.Instance.viewManager.SetFormationCanvasState(false);
        LobbyView.SetLobbyViewState(true);
    }
}
