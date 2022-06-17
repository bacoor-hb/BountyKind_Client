using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LocalGameController
{
    #region Lucky Draw
    public void Start_LuckyDraw()
    {
        Debug.Log("[LocalGameController] Start LuckyDraw...");
        LocalGameView.SetBtn_State(ACTION_TYPE.LUCKY_DRAW, false);
        GameEventController.HandleLuckyDraw();
    }

    public void End_LuckyDraw()
    {
        Debug.Log("[LocalGameController] End LuckyDraw...");
        LocalGameView.SetBtn_State(ACTION_TYPE.END_TURN, true);
    }
    #endregion
}
