using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalLobbyController : LocalSingleton<LocalLobbyController>
{
    [SerializeField]
    private LobbyView LobbyView;

    private List<BountyMap> bountyMaps;
    private string selectedMapKey;

    private UserDataManager UserDataManager;
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

        NetworkManager.OnGetMapSuccess = null;
        NetworkManager.OnGetMapSuccess += OnGetMapSuccess;
        NetworkManager.OnGetUserdataSuccess = null;
        NetworkManager.OnGetUserdataSuccess += OnGetUserDataAPI_Sucess;
        NetworkManager.OnLoginSuccess = null;
        NetworkManager.OnLoginSuccess += LoginSuccess;

        UserDataManager = GlobalManager.Instance.UserDataManager;
        LoadingManager = GlobalManager.Instance.LoadingManager;

        //Lobby View Setting
        InitLobbyView();
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
        Debug.Log("[RoomEventController] GetMap_FromServer: " + CONSTS.HOST_GET_MAP_API);
        NetworkManager.GetAllMap_FromAPI(CONSTS.HOST_GET_MAP_API);
        Debug.Log("[RoomEventController] GetUserData_FromServer");
        NetworkManager.GetUserData_FromAPI(CONSTS.HOST_GET_USERDATA_API, UserDataManager.UserData.address, UserDataManager.UserData.token);
    }
    #endregion

    #region ROOM Event
    /// <summary>
    /// Trigger the Create room.
    /// </summary>
    void CreateRoom()
    {
        if (bountyMaps != null && bountyMaps.Count > 0)
            NetworkManager.CreateRoom(ROOM_TYPE.GAME_ROOM, selectedMapKey);
        else
            Debug.LogError("[RoomEventController] Create Room ERROR: No Map Data.");
    }

    /// <summary>
    /// Trigger When a room is created
    /// </summary>
    void OnCreateRoomSuccess()
    {
        LoadingManager.LoadWithLoadingScene(SCENE_NAME.GameScene);
    }
    #endregion

    #region Return Event from Server API
    /// <summary>
    /// Trigger when the server return the map list: Update the map list on the UI.
    /// </summary>
    /// <param name="mapList"></param>
    void OnGetMapSuccess(List<BountyMap> mapList)
    {
        bountyMaps = mapList;

        LobbyView.SetLoobyFormStatus(true);
        LobbyView.SetLoginFormStatus(false);

        LobbyView.CreateRoom_Btn.enabled = true;
        LobbyView.UpdateMapList(mapList);

        selectedMapKey = bountyMaps[0].key;
        LobbyView.RoomType_DD.onValueChanged.RemoveAllListeners();
        LobbyView.RoomType_DD.onValueChanged.AddListener((currentMap) =>
        {
            selectedMapKey = bountyMaps[currentMap].key;
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
}
