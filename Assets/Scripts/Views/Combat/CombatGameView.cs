using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatGameView : InvitationPopupView
{
    public override void Init()
    {
        base.Init();
    }

    protected override void SetMessage(string _msg)
    {
        Debug.Log("[CombatGameView] SetMessage: " + _msg);
        base.SetMessage(_msg);
    }

    protected override void TriggerEngage(bool _skip)
    {
        Debug.Log("[CombatGameView] TriggerEngage: " + _skip);
        base.TriggerEngage(_skip);
    }
}
