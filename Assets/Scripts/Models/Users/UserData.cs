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
        address = "0x64470e5f5dd38e497194bbcaf8daa7ca578926f6";
        username = "Test Fake User";
        //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZGRyZXNzIjoiMHgwY2Y1OGNhMjlmZDgwOGJmOTU1OWMwN2NjN2QyYTRjYzgzMDA4MjI5IiwidXNlcm5hbWUiOiJsYWxhIiwiaXNSZWNlaXZlZCI6dHJ1ZSwiaW1hZ2UiOiJRbVhnVFlreUdTRnhhV0pvdk5aalpnRzdiTWZaZEVmMlNmUFhQN3J2OXBKNFcxIiwiaXNOZXdVc2VyIjpmYWxzZSwiaWF0IjoxNjU2NDc2NzM5LCJleHAiOjE5NzIwNTI3Mzl9.OuMFJRylYlPS-I2OA57jSS8r3oSH-kddPxzUSW2pq3I";
        token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZGRyZXNzIjoiMHg2NDQ3MGU1ZjVkZDM4ZTQ5NzE5NGJiY2FmOGRhYTdjYTU3ODkyNmY2IiwidXNlcm5hbWUiOiJuYW1lZSIsImltYWdlIjoiUW1XTTdrdkh5aHRuVEdkMk1lSDVmTFk3TWY0OWNLZ1hSd3NzTjFWdUxnSHVldiIsImlhdCI6MTY1NDg0NTgxOSwiZXhwIjoxOTcwNDIxODE5fQ.xvZ-3ZB-HEENh7s6scjDjsqcFVV524l3neCNirE67r4";
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
            energy = userData.energy
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