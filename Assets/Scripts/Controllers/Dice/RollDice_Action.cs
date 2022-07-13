using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BountyKind;

public class RollDice_Action : Action
{
    [SerializeField]
    private DicesController DicesController;

    private RollResult_MSG RollResultFromServer;
    public override void ClearEvent()
    {
        base.ClearEvent();
        RollResultFromServer = new RollResult_MSG();
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
        Debug.Log("[RollDice_Action] OnFinishRollingDice: " + _totalDice);
        turnBaseController.EndAction();
    }

    public override void SetActionData(object args)
    {
        base.SetActionData(args);

        RollResultFromServer = (RollResult_MSG)args;
    }
}
