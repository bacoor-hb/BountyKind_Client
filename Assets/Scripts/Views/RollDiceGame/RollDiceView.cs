using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollDiceView : InvitationPopupView
{
    public override void Init()
    {
        base.Init();
    }

    public override void SetMessage(string _msg)
    {
        Debug.Log("[RollDiceView] SetMessage: " + _msg);
        base.SetMessage(_msg);
    }

    public override void TriggerEngage()
    {
        Debug.Log("[RollDiceView] TriggerEngage");
        base.TriggerEngage();
    }

    public override void TriggerDecline()
    {
        Debug.Log("[RollDiceView] TriggerDecline");
        base.TriggerDecline();
    }
}
