using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    // Start is called before the first frame update\
    [DllImport("__Internal")]
    private static extern void Login();
    [DllImport("__Internal")]
    private static extern void Logout();
    [SerializeField]
    private Button LoginBTN;
    void Start()
    {
        LoginBTN.onClick.AddListener(() => { HandleLogin(); });   
    }

    public void HandleLogin()
    {
        InsertFakeData();
    }

    public void HandleLogout()  
    {
        Logout(); 
    }

    public void InsertFakeData()
    {
        UserData user = new UserData();
        user.address = "asd";
        user.sig = "asd";
        user.username = "asd";  
        user.token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZGRyZXNzIjoiMHg2NDQ3MGU1ZjVkZDM4ZTQ5NzE5NGJiY2FmOGRhYTdjYTU3ODkyNmY2IiwidXNlcm5hbWUiOiJuYW1lZSIsImltYWdlIjoiUW1XTTdrdkh5aHRuVEdkMk1lSDVmTFk3TWY0OWNLZ1hSd3NzTjFWdUxnSHVldiIsImlhdCI6MTY1MjE0NzY5MiwiZXhwIjoxOTY3NzIzNjkyfQ.iAvn0SgaQFPvzxtudPOaaw0SHKvPtLTmZ5gh-fwnpko";
        user.isSigned = true;
        user.tokenBalance.YU = 0;
        user.tokenBalance.YU2 = 0;
        user.tokenBalance.FFE = 0;
        user.rollNumber = 10;
        UserManagement.Instance.SetUser(user);
        SceneManager.LoadScene("Test_Login_Success");
    }

    public void LoginSuccess(string data)
    {
        UserData user = JsonUtility.FromJson<UserData>(data);
        user.rollNumber = 10;
        UserManagement.Instance.SetUser(user);
        SceneManager.LoadScene("Test_Login_Success");
    }

    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy login manager");
    }
}
