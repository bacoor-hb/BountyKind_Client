using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BountyKind;

public class EndTurnAction : Action
{
    public override void ClearEvent()
    {
        base.ClearEvent();
    }

    public override void InitAction(int _userId, TurnBaseController _controller)
    {
        base.InitAction(_userId, _controller);
        actionType = ACTION_TYPE.END_TURN;
    }

    public override void OnEndAction()
    {
        base.OnEndAction();
    }

    public override void OnStartAction()
    {
        base.OnStartAction();
        turnBaseController.EndAction();
    }
}
