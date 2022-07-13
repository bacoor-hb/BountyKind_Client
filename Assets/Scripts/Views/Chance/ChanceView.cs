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

    protected override void SetMessage(string _msg)
    {
        Debug.Log("[ChanceView] SetMessage: " + _msg);
        base.SetMessage(_msg);
    }

    protected override void TriggerEngage(bool _skip)
    {
        Debug.Log("[ChanceView] TriggerEngage: " + _skip);
        base.TriggerEngage(_skip);
    }
}
