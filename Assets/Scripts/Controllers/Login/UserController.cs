using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class UserController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Logout();
    // Start is called before the first frame update
    [SerializeField]
    private InputField address;
    [SerializeField]
    private InputField username;
    [SerializeField]
    private InputField balanceYU;
    [SerializeField]
    private InputField balanceYU2;
    [SerializeField]
    private InputField balanceFFE;
    [SerializeField]
    private Button LogoutBTN;
    [SerializeField]
    private Button CreateRoomBTN;
    [SerializeField]
    private GameObject bountyColyseusManagerObj;

    private UserDataManager UserDataManager;
    private NetworkManager NetworkManager;
    private LoadingManager LoadingManager;

    void Start()
    {
        FillData();
        UserDataManager = GlobalManager.Instance.UserDataManager;
        NetworkManager = GlobalManager.Instance.NetworkManager;
        LoadingManager = GlobalManager.Instance.LoadingManager;

        Debug.Log("User controller");
        
        NetworkManager.Connect();
        NetworkManager.JoinLobby();
       
        LogoutBTN.onClick.AddListener(() => { HandleLogout(); });
        CreateRoomBTN.onClick.AddListener(() => { HandleCreateRoom(); });

        
    }

    void FillData()
    {
        address.text = UserDataManager.UserData.address;
        username.text = UserDataManager.UserData.username;
        balanceYU.text = UserDataManager.UserData.tokenBalance.YU.ToString();
        balanceYU2.text = UserDataManager.UserData.tokenBalance.YU2.ToString();
        balanceFFE.text = UserDataManager.UserData.tokenBalance.FFE.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HandleLogout()
    {
        HandleLogoutEvent();
        Logout();
    }

    void HandleCreateRoom()
    {
        LoadingManager.LoadWithLoadingScene(SCENE_NAME.Test_Create_Room);
    }
    public void HandleLogoutEvent()
    {
        NetworkManager.SetUser(null);        
        NetworkManager.Disconnect();

        LoadingManager.LoadWithLoadingScene(SCENE_NAME.Test_Login);
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy user controller");
    }
}
