using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDice_Action : Action
{
    [SerializeField]
    private DicesController DicesController;
    public override void ClearEvent()
    {
        base.ClearEvent();
        DicesController.ResetDicesPosition();

        DicesController.OnAllDiceFinishAnim = null;
        DicesController.OnAllDiceFinishAnim += OnFinishRollingDice;
    }

    public override void InitAction(int _userId, TurnBaseController _controller)
    {
        base.InitAction(_userId, _controller);
        actionType = ACTION_TYPE.ROLL_DICE;
    }

    public override void OnEndAction()
    {
        base.OnEndAction();
    }

    public override void OnStartAction()
    {
        base.OnStartAction();
    }

    private void OnFinishRollingDice(int _totalDice)
    {
        turnBaseController.EndAction();
    }
}
