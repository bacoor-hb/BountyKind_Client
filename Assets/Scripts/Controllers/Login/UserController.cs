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

    void Start()
    {
        FillData();
        Debug.Log("User controller");
        if (UserManagement.Instance.GetSocketManager() == null)
        {
            UserManagement.Instance.InitializeSocketManager();
            UserManagement.Instance.Connect();
            UserManagement.Instance.JoinLobby();
        }
        else
        {
            Debug.Log("Existed");
        }
        LogoutBTN.onClick.AddListener(() => { HandleLogout(); });
        CreateRoomBTN.onClick.AddListener(() => { HandleCreateRoom(); });
    }

    void FillData()
    {
        address.text = UserManagement.Instance.GetUser().address;
        username.text = UserManagement.Instance.GetUser().username;
        balanceYU.text = UserManagement.Instance.GetUser().tokenBalance.YU.ToString();
        balanceYU2.text = UserManagement.Instance.GetUser().tokenBalance.YU2.ToString();
        balanceFFE.text = UserManagement.Instance.GetUser().tokenBalance.FFE.ToString();
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
        SceneManager.LoadScene("Test_Create_Room");
    }
    public void HandleLogoutEvent()
    {
        UserManagement.Instance.SetUser(null);
        SceneManager.LoadScene("Test_Login");
        UserManagement.Instance.Disconnect();
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy user controller");
    }
}
