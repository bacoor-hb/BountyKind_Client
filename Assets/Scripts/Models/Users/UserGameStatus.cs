using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGameStatus
{
    public string currentMapKey;
    public BountyMap currentMap;

    public int currentNode;
    public int totalStep;
    public int totalRoll;

    public List<Character> Characters;
    public List<FormationCharacter> FormationList;

    public UserGameStatus()
    {
        currentNode = -1;
        totalStep = -1;
        totalRoll = -1;

        Characters = new List<Character>();
        FormationList = new List<FormationCharacter>();
    }

    /// <summary>
    /// Update the current Game Status of the player
    /// </summary>
    /// <param name="rollResult"></param>
    public void UpdateGameStatus(RollResult_MSG rollResult)
    {
        currentNode = Mathf.RoundToInt(rollResult.currentNode);
        totalStep = Mathf.RoundToInt(rollResult.totalStep);
        totalRoll = Mathf.RoundToInt(rollResult.totalRoll);
    }

    public void UpdateMapKey(string mapKey)
    {
        currentMapKey = mapKey;
    }

    public void UpdateMapData()
    {
        MapNodeDataManager mapNodeData = GlobalManager.Instance.MapNodeDataManager;
        if (mapNodeData != null
            && mapNodeData.cachedMap.ContainsKey(currentMapKey))
        {
            currentMap = mapNodeData.cachedMap[currentMapKey];
            Debug.Log("[UserGameStatus] Update map success..." + currentMapKey);
        }
        else
        {
            Debug.LogError("[UserGameStatus] UpdateMap ERROR: map not exists.");
        }
    }

    public ACTION_TYPE Get_MapActionType()
    {
        if(string.IsNullOrEmpty(currentMapKey)
            || currentMap == null
            || currentNode < 0)
        {
            return ACTION_TYPE.INVALID_ACTION;
        }

        var MathAction = currentMap.nodes[currentNode].type;
        switch (MathAction)
        {
            case TypeNode.START:
                return ACTION_TYPE.END_TURN;
                break;
            case TypeNode.CHANCE:
                return ACTION_TYPE.CHANCE;
                break;
            case TypeNode.LUCKY_DRAW:
                return ACTION_TYPE.LUCKY_DRAW;
                break;
            case TypeNode.BOSS:
                return ACTION_TYPE.COMBAT;
                break;
            case TypeNode.DEFAULT:
                return ACTION_TYPE.END_TURN;
                break;
            default:
                return ACTION_TYPE.INVALID_ACTION;
                break;
        }

        
    }
}
