using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LocalGameController
{
    #region Dice Controller
    int[] DicesValue;
    public void Call_RollDice()
    {
        Debug.Log("[LocalGameController] Call_RollDice");
        RollDiceView rollView = LocalGameView.RollDiceView;
        bool skip = currentPlayer.skipRollDice;
        rollView.ClosePopup();

        rollView.OnClosePopupFinish = null;
        rollView.OnClosePopupFinish += () =>
        {
            if(!skip)
            {
                Multiplayer_GameEvent.HandleRoll();
                rollView.ClosePopup();
                rollView.OnClosePopupFinish = null;
                rollView.OnClosePopupFinish += (() => finishRollPopupAnim = true);
            }  
        }; 
    }

    public void RollDice(RollResult_MSG rollResult)
    {
        DicesValue = new int[1];
        DicesValue[0] = Mathf.RoundToInt(rollResult.rollResult);

        StartCoroutine(WaitingForRollResult());
    }

    bool finishRollPopupAnim;
    /// <summary>
    /// Waiting for the popup Roll Dice finish
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitingForRollResult()
    {
        while(!finishRollPopupAnim)
        {
            yield return new WaitForSeconds(0.1f);  
        }
        //Start Roll Dice
        DicesController.RollDice(DicesValue);
    }

    public void End_RollDice()
    {
        Debug.Log("[LocalGameController] End_RollDice: " + DicesValue.Length);
        if (DicesValue != null && DicesValue.Length > 0)
        {
            Move_Action();
        }
    }
    #endregion
}
