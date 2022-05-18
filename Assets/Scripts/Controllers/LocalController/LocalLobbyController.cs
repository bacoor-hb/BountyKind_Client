using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalLobbyController : LocalSingleton<LocalLobbyController>
{
    [SerializeField]
    private LoginManager LoginManager;
    [SerializeField]
    private RoomEventController RoomEventController;
    [SerializeField]
    private LobbyView LobbyView;

    private NetworkManager NetworkManager;
    private LoadingManager LoadingManager;
    // Start is called before the first frame update
    void Start()
    {
        LoginManager.Init();
        RoomEventController.Init();
        LobbyView.Init();

        NetworkManager = GlobalManager.Instance.NetworkManager;
        BountyColyseusManager.Instance.OnJoinLobbySuccess = null;
        BountyColyseusManager.Instance.OnJoinLobbySuccess += OnConnectSuccess;

        BountyColyseusManager.Instance.OnJoinRoomSuccess = null;
        BountyColyseusManager.Instance.OnJoinRoomSuccess += OnCreateRoomSuccess;

        LoadingManager = GlobalManager.Instance.LoadingManager;
    }

    void OnCreateRoomSuccess()
    {
        LoadingManager.LoadWithLoadingScene(SCENE_NAME.GameScene);
    }

    void OnConnectSuccess()
    {
        RoomEventController.GetRoomType();
    }
}
