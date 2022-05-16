using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Login();
    [DllImport("__Internal")]
    private static extern void Logout();
    [SerializeField]
    private Button LoginBTN;

    private LoadingManager LoadingManager;
    private NetworkManager NetworkManager;
    private UserDataManager UserDataManager;
    void Start()
    {
        LoadingManager = GlobalManager.Instance.LoadingManager;
        NetworkManager = GlobalManager.Instance.NetworkManager;
        UserDataManager = GlobalManager.Instance.UserDataManager;

        LoginBTN.onClick.AddListener(() => { HandleLogin(); });   
    }

    public void HandleLogin()
    {
#if TEST_FAKE_DATA
        UserData user = new UserData();   
        user.GenerateFakeData();
        SetLoginData(user);
#endif
    }

    public void HandleLogout()  
    {
        Logout(); 
    }    

    /// <summary>
    /// Called by Web Client when Login Success
    /// </summary>
    /// <param name="data"></param>
    public void LoginSuccess(string data)
    {
        UserData user = JsonUtility.FromJson<UserData>(data);
        user.rollNumber = 10;
        SetLoginData(user);
    }

    private void SetLoginData(UserData _dataReceived)
    {
        UserDataManager.SetUserData(_dataReceived);
        LoadingManager.LoadWithLoadingScene(SCENE_NAME.Test_Login_Success);
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Debug.Log("[LoginManager] Destroyed...");
    }
}
