using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public UserData UserData { get; private set; }

    // Start is called before the first frame update
    public void Init()
    {
        UserData = new UserData();
    }

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
}
