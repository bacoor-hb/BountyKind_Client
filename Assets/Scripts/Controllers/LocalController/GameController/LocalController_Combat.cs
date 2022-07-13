using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LocalGameController
{
    #region Combat Control
    public void Start_Combat(bool _skip)
    {
        if(_skip)
        {
            Debug.Log("[LocalGameController] Start Combat...");            
        }
        else
        {
            Debug.Log("[LocalGameController] Deny Combat...");
        }
        GameEventController.Handle_Combat(_skip);
    }

    public void Deny_Combat()
    {
        
    }

    public void End_Combat()
    {
        Debug.Log("[LocalGameController] End Combat...");
        //LocalGameView.SetBtn_State(ACTION_TYPE.END_TURN, true);
        EndTurn_Action();
    }

    private void Combat_GetData(Battle_MSG battleData)
    {

    }
    #endregion
}
