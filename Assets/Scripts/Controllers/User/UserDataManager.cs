using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public UserData UserData { get; private set; }

    public void Init()
    {
        UserData = new UserData();
    }

    /// <summary>
    /// Set the User Data directly.
    /// </summary>
    /// <param name="_data"></param>
    public void SetUserData(UserData _data)
    {
        UserData = _data;
    }

    /// <summary>
    /// Get the user Data by its UserID. Can exist more than 1 player in the Multiplayer mode.
    /// </summary>
    /// <param name="_localUserID"></param>
    /// <returns></returns>
    public UserData GetUserData(int _localUserID)
    {
        //Only for TEST. Should return the exact player data by its local ID.
        return UserData;
    }

    /// <summary>
    /// Updte the Roll Number
    /// </summary>
    /// <param name="rollNB"></param>
    public void SetRollNumber(int rollNB)
    {
        if(UserData != null)
        {
            UserData.rollNumber = rollNB;
        }
    }
}
