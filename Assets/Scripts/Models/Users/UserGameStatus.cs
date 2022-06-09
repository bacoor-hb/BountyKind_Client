using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGameStatus
{
    public string currentMap;

    public int currentNode;
    public int totalStep;
    public int totalRoll;

    public List<Character> Characters;

    public UserGameStatus()
    {
        currentNode = -1;
        totalStep = -1;
        totalRoll = -1;

        Characters = new List<Character>();
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
}
