using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BountyKind;

public class LuckyDrawAction : Action
{
    public override void InitAction(int _userId, TurnBaseController _controller)
    {
        base.InitAction(_userId, _controller);
        actionType = ACTION_TYPE.LUCKY_DRAW;
    }

    public override void OnEndAction()
    {
        base.OnEndAction();
    }

    public override void OnStartAction()
    {
        base.OnStartAction();
    }

    public void OnLuckyDraw_Return(Reward_MSG msg)
    {
        string result = JsonUtility.ToJson(msg);
        Debug.Log("[LuckyDrawAction][OnLuckyDraw_Return] " + result);

        turnBaseController.EndAction();
    }
}