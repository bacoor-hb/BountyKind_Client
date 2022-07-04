using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChanceView : InvitationPopupView
{
    public override void Init()
    {
        base.Init();
    }

    public override void SetMessage(string _msg)
    {
        Debug.Log("[ChanceView] SetMessage: " + _msg);
        base.SetMessage(_msg);
    }

    public override void TriggerEngage()
    {
        Debug.Log("[ChanceView] TriggerEngage");
        base.TriggerEngage();
    }

    public override void TriggerDecline()
    {
        Debug.Log("[ChanceView] TriggerDecline");
        base.TriggerDecline();
    }
}
