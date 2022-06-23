using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LocalGameController
{
    #region Combat Control
    public void Start_Combat()
    {
        Debug.Log("[LocalGameController] Start Combat...");
        LocalGameView.SetBtn_State(ACTION_TYPE.COMBAT, false);
        GameEventController.Handle_Combat();
    }

    public void End_Combat()
    {
        Debug.Log("[LocalGameController] End Combat...");
        LocalGameView.SetBtn_State(ACTION_TYPE.END_TURN, true);
    }
    #endregion
}
