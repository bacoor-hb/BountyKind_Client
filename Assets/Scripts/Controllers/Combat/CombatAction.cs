using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BountyKind;

public class CombatAction : Action
{
    public override void InitAction(int _userId, TurnBaseController _controller)
    {
        base.InitAction(_userId, _controller);
        actionType = ACTION_TYPE.COMBAT;
    }

    public override void OnEndAction()
    {
        base.OnEndAction();
    }

    public override void OnStartAction()
    {
        base.OnStartAction();
        StartCoroutine(Combat_Process());
    }

    /// <summary>
    /// TO DO: Implement Combat process
    /// </summary>
    /// <returns></returns>
    IEnumerator Combat_Process()
    {
        Debug.Log("[CombatAction] Processing...");
        yield return new WaitForSeconds(1.0f);
        turnBaseController.EndAction();
    }
}
