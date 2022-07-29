using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string address;
    public string username;
    public string token;
    public string img;
    public string _id;
    public int ranking;
    public int currentGameId;
    public int BWP;
    public TokenBalance tokenBalance;

    /// <summary>
    /// Only for TEST
    /// </summary>
    /// <returns></returns>
    public void GenerateFakeData()
    {
        //address = "0x0Cf58ca29fd808Bf9559C07CC7D2A4cc83008229";
        address = "0x64470E5F5DD38e497194BbcAF8Daa7CA578926F6";
        username = "Test Fake User";
        //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZGRyZXNzIjoiMHgwY2Y1OGNhMjlmZDgwOGJmOTU1OWMwN2NjN2QyYTRjYzgzMDA4MjI5IiwidXNlcm5hbWUiOiJsYWxhIiwiaXNSZWNlaXZlZCI6dHJ1ZSwiaW1hZ2UiOiJRbVhnVFlreUdTRnhhV0pvdk5aalpnRzdiTWZaZEVmMlNmUFhQN3J2OXBKNFcxIiwiaXNOZXdVc2VyIjpmYWxzZSwiaWF0IjoxNjU2NDc2NzM5LCJleHAiOjE5NzIwNTI3Mzl9.OuMFJRylYlPS-I2OA57jSS8r3oSH-kddPxzUSW2pq3I";
        token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI2MmUwMTViMGM5Y2Q3NDc5MzJjYTgwYjQiLCJhZGRyZXNzIjoiMHg2NDQ3MGU1ZjVkZDM4ZTQ5NzE5NGJiY2FmOGRhYTdjYTU3ODkyNmY2IiwiaXNSZWNlaXZlZCI6ZmFsc2UsInVzZXJuYW1lIjoiMHg2NDQ3MGU1ZjVkZDM4ZTQ5NzE5NGJiY2FmOGRhYTdjYTU3ODkyNmY2IiwiaXNOZXdVc2VyIjpmYWxzZSwiaWF0IjoxNjU5MDU5Mjg0LCJleHAiOjE5NzQ2MzUyODR9.bH_dWV3iz-3--fDN0cSc5UvLUs-cNlZ7WJ-OuVsfT1s";
        _id = "61ee1891d1949c6bc79425a8";
        BWP = 0;
        currentGameId = -1;
        ranking = 1;

        TokenBalance tokenBalance = new TokenBalance()
        {
            yuPoint = 0,
            energy = 0
        };

        this.tokenBalance = tokenBalance;
    }

    public UserData()
    {

    }

    public void SetUserData_API(UserData_API userData)
    {
        address = userData.address;
        username = userData.username;
        ranking = userData.ranking;
        currentGameId = userData.currentGameId;
        BWP = userData.bwp;

        tokenBalance = new TokenBalance()
        {
            yuPoint = userData.yuPoint,
            energy = userData.energy,
        };
        _id = userData._id;
    }

    public void SetUserData_FromWeb(UserData_WebLogin webLogin)
    {
        address = webLogin.address;
        username = webLogin.userName;
        img = webLogin.img;
        token = webLogin.token;
    }

    public void UpdateBalance(TokenBalance _balance)
    {
        tokenBalance = _balance;
    }
}

[Serializable]
public class UserData_MSG
{
    public UserData_API data;
    public string message;
}

[Serializable]
public class UserData_API
{
    public int bwp;
    public int energy;
    public int yuPoint;
    public int ranking;
    public int currentGameId;
    public string _id;
    public string address;
    public string username;
}

[Serializable]
public class UserData_WebLogin
{
    public string address;
    public string userName;
    public string img;
    public string token;
}

[Serializable]
public class UserCharactersResponse
{
    public UserCharacters_API data;
    public string message;
}