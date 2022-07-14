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
        //address = "0x64470e5f5dd38e497194bbcaf8daa7ca578926f6";
        address = "0x0Cf58ca29fd808Bf9559C07CC7D2A4cc83008229";
        username = "Test Fake User";
        token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZGRyZXNzIjoiMHgwY2Y1OGNhMjlmZDgwOGJmOTU1OWMwN2NjN2QyYTRjYzgzMDA4MjI5IiwidXNlcm5hbWUiOiJsYWxhIiwiaXNSZWNlaXZlZCI6dHJ1ZSwiaW1hZ2UiOiJRbVhnVFlreUdTRnhhV0pvdk5aalpnRzdiTWZaZEVmMlNmUFhQN3J2OXBKNFcxIiwiaXNOZXdVc2VyIjpmYWxzZSwiaWF0IjoxNjU2NDc2NzM5LCJleHAiOjE5NzIwNTI3Mzl9.OuMFJRylYlPS-I2OA57jSS8r3oSH-kddPxzUSW2pq3I";
        //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZGRyZXNzIjoiMHg2NDQ3MGU1ZjVkZDM4ZTQ5NzE5NGJiY2FmOGRhYTdjYTU3ODkyNmY2IiwidXNlcm5hbWUiOiJuYW1lZSIsImltYWdlIjoiUW1XTTdrdkh5aHRuVEdkMk1lSDVmTFk3TWY0OWNLZ1hSd3NzTjFWdUxnSHVldiIsImlhdCI6MTY1NDg0NTgxOSwiZXhwIjoxOTcwNDIxODE5fQ.xvZ-3ZB-HEENh7s6scjDjsqcFVV524l3neCNirE67r4";
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

    public void SetUserData_API(UserData_API userData)
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
public class UserCharactersResponse
{
    public UserCharacters_API data;
    public string message;
}

[Serializable]
public class UserCharacters_API
{
    public UserCharacter[] data;
    public int limit;
    public int page;
    public int total;
    public int pages;
}

[Serializable]
public class UserItemsResponse
{
    public UserItems_API data;
    public string message;
}

[Serializable]
public class UserItems_API
{
    public UserItem[] data;
    public int limit;
    public int page;
    public int total;
    public int pages;
}
[Serializable]
public class UserItem
{
    public string _id;
    public string key;
    public string baseKey;
    public string name;
    public string image;
    public string status;
    public string type;
    public string contractAddress;
    public string nftId;
    public string ownerAddress;
}

[Serializable]
public class UserCharacter
{
    public string _id;
    public string key;
    public string baseKey;
    public string name;
    public string status;
    public int level;
    public string race;
    public string element;
    public string type;
    public string contractAddress;
    public string nftId;
    public string ownerAddress;
    public string image;
    public UserCharacter()
    {
        _id = null;
        key = null;
        baseKey = null;
        name = null;
        status = null;
        level = 0;
        race = null;
        element = null;
        type = null;
        contractAddress = null;
        nftId = null;
        ownerAddress = null;
        image = null;
    }
}

[Serializable]
public class UserFormationResponse
{
    public string message;
    public FormationCharacters[] data;
}

[Serializable]
public class FormationCharacters
{
    public string _id;
    public string key;
    public string baseKey;
    public string status;
    public string[] itemList;
    public int atk;
    public int def;
    public int speed;
    public int hp;
    public int level;
    public string contractAddress;
    public string nftId;
    public int position;

    public FormationCharacters() { }
    public FormationCharacters(string __id, string _key, string _baseKey, string _status, string[] _itemList, int _atk, int _def, int _speed, int _hp, int _level, string _contractAddress, string _nftId, int _position)
    {
        _id = __id;
        key = _key;
        baseKey = _baseKey;
        status = _status;
        itemList = _itemList;
        atk = _atk;
        def = _def;
        speed = _speed;
        hp = _hp;
        level = _level;
        contractAddress = _contractAddress;
        nftId = _nftId;
        position = _position;
    }
}
[Serializable]
public class CharacterWithPositionList
{
    public List<CharacterWithPosition> data;
    public CharacterWithPositionList()
    {
        data = new List<CharacterWithPosition>();
    }
}
[Serializable]
public class CharacterWithPosition
{
    public string characterId;
    public int position;
    public CharacterWithPosition(string _characterId, int _position)
    {
        characterId = _characterId;
        position = _position;
    }
}
[Serializable]
public class SetFormationResponse
{
    public string message;
}