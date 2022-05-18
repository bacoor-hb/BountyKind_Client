using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    public string address;
    public string sig;
    public string username;
    public string token;
    public bool isSigned;
    public TokenBalance tokenBalance;

    /// <summary>
    /// Only for TEST
    /// </summary>
    /// <returns></returns>
    public UserData GenerateFakeData()
    {
        UserData user = new UserData
        {
            address = "asd",
            sig = "asd",
            username = "asd",
            token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZGRyZXNzIjoiMHg2NDQ3MGU1ZjVkZDM4ZTQ5NzE5NGJiY2FmOGRhYTdjYTU3ODkyNmY2IiwidXNlcm5hbWUiOiJuYW1lZSIsImltYWdlIjoiUW1XTTdrdkh5aHRuVEdkMk1lSDVmTFk3TWY0OWNLZ1hSd3NzTjFWdUxnSHVldiIsImlhdCI6MTY1MjE0NzY5MiwiZXhwIjoxOTY3NzIzNjkyfQ.iAvn0SgaQFPvzxtudPOaaw0SHKvPtLTmZ5gh-fwnpko",
            isSigned = true,
        };

        TokenBalance tokenBalance = new TokenBalance()
        {
            YU = 0,
            YU_Point = 0,
            FFE = 0,
            FFE_Point = 10
        };

        user.tokenBalance = tokenBalance;

        return user;
    }
}