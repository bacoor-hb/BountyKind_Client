using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatGameView : InvitationPopupView
{
    public override void Init()
    {
        base.Init();
    }

    public override void SetMessage(string _msg)
    {
        Debug.Log("[CombatGameView] SetMessage: " + _msg);
        base.SetMessage(_msg);
    }

    public override void TriggerEngage()
    {
        Debug.Log("[CombatGameView] TriggerEngage");
        base.TriggerEngage();
    }

    public override void TriggerDecline()
    {
        Debug.Log("[CombatGameView] TriggerDecline");
        base.TriggerDecline();
    }
}
