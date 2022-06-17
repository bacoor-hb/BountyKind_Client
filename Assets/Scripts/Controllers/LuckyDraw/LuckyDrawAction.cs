using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        StartCoroutine(LuckyDraw_Process());
    }

    /// <summary>
    /// TO DO: Implement Lucky draw process.
    /// </summary>
    /// <returns></returns>
    IEnumerator LuckyDraw_Process()
    {
        Debug.Log("[LuckyDrawAction] Processing...");

        yield return new WaitForSeconds(1.0f);
        turnBaseController.EndAction();
    }
}
