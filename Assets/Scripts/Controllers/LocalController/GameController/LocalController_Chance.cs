using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LocalGameController
{
    #region Chance
    public void Start_Chance()
    {
        Debug.Log("[LocalGameController] Start Chance...");
        LocalGameView.SetBtn_State(ACTION_TYPE.CHANCE, false);
        GameEventController.Handle_Chance();
    }

    public void End_Chance()
    {
        Debug.Log("[LocalGameController] End Chance...");
        LocalGameView.SetBtn_State(ACTION_TYPE.END_TURN, true);
    }
    #endregion
}
