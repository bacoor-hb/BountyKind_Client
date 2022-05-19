using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    [SerializeField]
    private LobbyView LobbyView;

    private NetworkManager NetworkManager;
    private UserDataManager UserDataManager;
    public void Init()
    {
        NetworkManager = GlobalManager.Instance.NetworkManager;
        UserDataManager = GlobalManager.Instance.UserDataManager;

        LobbyView.Login_Btn.onClick.AddListener(() => { HandleLogin(); });   
        LobbyView.Logout_Btn.onClick.AddListener(() => { HandleLogout(); });

        NetworkManager.OnLoginSuccess = null;
        NetworkManager.OnLoginSuccess += LoginSuccess;
    }

    public void HandleLogin()
    {
#if UNITY_EDITOR
        UserData user = new UserData();
        user = UserData.GenerateFakeData();
        NextLoginStep(user);
#else
        NetworkManager.Login_External();
#endif
    }

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
        UserData user = JsonUtility.FromJson<UserData>(data);
        NextLoginStep(user);
    }

    private void NextLoginStep(UserData _dataReceived)
    {
        UserDataManager.SetUserData(_dataReceived);

        NetworkManager.Connect();
    }
}
