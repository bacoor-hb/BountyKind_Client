using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public UserData UserData { get; private set; }
    public UserGameStatus UserGameStatus { get; private set; }
    public void Init()
    {
        UserData = new UserData();
        UserGameStatus = new UserGameStatus();
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
    /// Update the Energy Value
    /// </summary>
    /// <param name="_energy"></param>
    public void SetEnergy(int _energy)
    {
        if(UserData != null)
        {
            UserData.tokenBalance.Energy = _energy;
        }
    }

    public int GetEnergy()
    {
        if(UserData != null && UserData.tokenBalance != null)
        {
            return UserData.tokenBalance.Energy;
        }
        else
        {
            return -1;
        }
    }

    /// <summary>
    /// Set the selected map key
    /// </summary>
    /// <param name="mapKey"></param>
    public void SetCurrentMap(string mapKey)
    {
        UserGameStatus.UpdateMapKey(mapKey);
    }

    /// <summary>
    /// Get the map key selected
    /// </summary>
    /// <returns></returns>
    public string GetCurrentMapId()
    {
        return UserGameStatus.currentMapKey;
    }

    /// <summary>
    /// Get the current map detail
    /// </summary>
    /// <returns></returns>
    public BountyMap GetCurrentMap()
    {
        return UserGameStatus.currentMap;
    }

    /// <summary>
    /// Get the list of next nodes of the current Map
    /// </summary>
    /// <param name="currentNode"></param>
    /// <returns></returns>
    public List<int> GetNextNodes(int currentNode)
    {
        return UserGameStatus.currentMap.nodes[currentNode].nextNode;
    }

    /// <summary>
    /// Get the list of previous nodes of the current Map
    /// </summary>
    /// <param name="currentNode"></param>
    /// <returns></returns>
    public List<int> GetPreNodes(int currentNode)
    {
        return UserGameStatus.currentMap.nodes[currentNode].preNode;
    }
}
