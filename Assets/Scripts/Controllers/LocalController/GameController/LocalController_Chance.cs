using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LocalGameController
{
    #region Chance
    public void Start_Chance()
    {
        Debug.Log("[LocalGameController] Start Chance...");
        //LocalGameView.SetBtn_State(ACTION_TYPE.CHANCE, false);
        bool _skip = currentPlayer.skipChance;
        ChanceView chanceView = LocalGameView.ChanceView;
        chanceView.ClosePopup();

        chanceView.OnClosePopupFinish = null;
        chanceView.OnClosePopupFinish += () =>
        {
            chanceView.OnClosePopupFinish = null;
            GameEventController.Handle_Chance(_skip);
        };
    }

    public void End_Chance()
    {
        Debug.Log("[LocalGameController] End Chance...");
        //LocalGameView.SetBtn_State(ACTION_TYPE.END_TURN, true);
        EndTurn_Action();
    }

    private void Chance_GetReward(Reward_MSG reward)
    {
        Debug.Log("[LocalGameController] Chance_GetReward: " + JsonUtility.ToJson(reward));
        TurnBaseController.EndAction();
    }
    #endregion
}
