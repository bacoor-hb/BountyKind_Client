using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LocalGameController
{
    #region Dice Controller
    int[] DicesValue;
    /// <summary>
    /// Only for TEST. The real data will be got from Server. 
    /// </summary>
    /// <returns></returns>
    public int[] GetDiceValue(int _diceNB = 1)
    {
        if (_diceNB > 0)
        {
            DicesValue = new int[_diceNB];
            for (int i = 0; i < _diceNB; i++)
            {
                DicesValue[i] = Random.Range(1, 7);
            }
            return DicesValue;
        }
        else
        {
            return null;
        }
    }

    public void Call_RollDice()
    {
        Debug.Log("[LocalGameController] Call_RollDice");
        Multiplayer_GameEvent.HandleRoll();

        //Deactivate the Roll button if the roll number is 0
        if (rollNumber <= 0)
        {
            LocalGameView.SetBtn_State(ACTION_TYPE.ROLL_DICE, false);
        }
    }

    public void RollDice(RollResult_MSG rollResult)
    {
        DicesValue = new int[1];
        DicesValue[0] = Mathf.RoundToInt(rollResult.rollResult);
        DicesController.RollDice(DicesValue);
    }

    public void End_RollDice()
    {
        if (DicesValue != null && DicesValue.Length > 0)
        {
            LocalGameView.SetBtn_State(ACTION_TYPE.MOVE, true);
        }
    }
    #endregion
}
