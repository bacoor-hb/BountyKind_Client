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
        address = "0x7e1ba029f0d2fb3833f8d7f291ef7e4516037289";
        username = "Test User";
        token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZGRyZXNzIjoiMHg3ZTFiYTAyOWYwZDJmYjM4MzNmOGQ3ZjI5MWVmN2U0NTE2MDM3Mjg5IiwiaWF0IjoxNjUwNTM2NTY5LCJleHAiOjE5NjYxMTI1Njl9.TCWBSo-gK72h1kl2EPEE9gbShPODvE4MgEeNV0rM0Xg";
        _id = "61ee1891d1949c6bc79425a8";
        BWP = 0;
        currentGameId = -1;
        ranking = 1;       

        TokenBalance tokenBalance = new TokenBalance()
        {
            YU_Point = 0,
            Energy = 0
        };

        this.tokenBalance = tokenBalance;
    }

    public UserData()
    {

    }

    public void  SetUserData_API (UserData_API userData)
    {
        address = userData.address;
        username = userData.username;
        ranking = userData.ranking;
        currentGameId = userData.currentGameId;
        BWP = userData.bwp;

        tokenBalance = new TokenBalance()
        {
            YU_Point = userData.yuPoint,
            Energy = userData.energy
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
public class MapShortList_MSG
{
    public MapShort_MSG[] maps;
}

[Serializable]
public class MapShort_MSG
{
    public string key;
    public string name;
    public float totalNode;

    public MapShort_MSG(string _key, string _name, float _totalNode) {
        key = _key;
        name = _name;
        totalNode = _totalNode;
    }
}

[Serializable]
public class Map_MSG
{
    public string key;
    public string name;
    public float totalNode;
    public Node_MSG[] nodes;
}

public class Node_MSG
{
    public string key;
    public string name;
    public float position;
    public string type;
    public Character_MSG[] enemy = {};
    public float[] preNode;
    public float[] nextNode;
}

public class Status_MSG
{
    public float atk;
    public float def;
    public float speed;
    public float hp;
}
public class Character_MSG: Status_MSG
{
    public string key;
    public string name;
    public float position;
    public float level;
}