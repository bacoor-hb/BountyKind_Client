using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action
{
    [SerializeField]
    private MovementController MovementController;
    public override void ClearEvent()
    {
        base.ClearEvent();
    }

    public override void InitAction(int _userId, TurnBaseController _controller)
    {
        base.InitAction(_userId, _controller);
        actionType = ACTION_TYPE.MOVE;
    }

    public override void OnEndAction()
    {
        base.OnEndAction();
    }

    public override void OnStartAction()
    {
        base.OnStartAction();
    }
}
