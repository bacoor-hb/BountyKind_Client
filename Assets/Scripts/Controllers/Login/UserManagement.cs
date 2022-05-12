using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class TokenBalance
{
    public float YU;
    public float YU2;
    public float FFE;
    public TokenBalance (float _YU, float _YU2, float _FFE) {
        YU = _YU;
        YU2 = _YU2;
        FFE = _FFE;
    }
}

[Serializable]
public class UserData
{
    public string address;
    public string sig;
    public string username;
    public string token;
    public bool isSigned;
    public TokenBalance tokenBalance = new TokenBalance(0, 0, 0);
    public int rollNumber;
}

public class UserManagement : GlobalSingleton<UserManagement>
{
    [DllImport("__Internal")]
    private static extern void Refill();
    private UserData user;
    [SerializeField]
    private BountyColyseusManager socketManager;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetUser(UserData _user)
    {
        user = _user;
    }

    public void SetRollNumber(int _rollNumber)
    {
        user.rollNumber = _rollNumber;
    }

    public void SetTokenBalance(TokenBalance _tokenBalance)
    {
        user.tokenBalance = _tokenBalance;
    }

    public BountyColyseusManager GetSocketManager()
    {
        return socketManager;
    }

    public void InitializeSocketManager()
    {
        socketManager = gameObject.AddComponent<BountyColyseusManager>();
    }

    public void Connect()
    {
        socketManager.Connect();
    }

    public void JoinLobby()
    {
        socketManager.JoinLobby(this.user.token);
    }

    public void CreateRoom(string roomType, string mapKey)
    {
        socketManager.CreateRoom(roomType, mapKey, this.user.token);
    }
    public async void Roll()
    {
        if (this.user.rollNumber > 0)
        {
            await socketManager.gameRoom.Send(PLAYER_SENT_EVENTS.ROLL_DICE);
            user.rollNumber--;
        }
        else
        {
            Refill();
        }
    }
    public async void ExitGame()
    {
        SceneManager.LoadScene("Test_Login_Success");
    }

    public void Disconnect()
    {
        socketManager.Disconnect();
    }

    public UserData GetUser()
    {
        return user;
    }
}
