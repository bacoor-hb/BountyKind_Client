using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuctionActionTest : Action
{
    public override void InitAction(int _userId, TurnBaseController _controller)
    {
        base.InitAction(_userId, _controller);
    }

    public override void ClearEvent()
    {
        base.ClearEvent();
    }

    public override void OnEndAction()
    {
        base.OnEndAction();
    }

    public override void OnStartAction()
    {
        base.OnStartAction();
    }

    int auctionActionFlag = 0;
    public void OnAuctionAction()
    {
        auctionActionFlag = 0;
        StartCoroutine(OnAunction1());
        StartCoroutine(OnAunction2());
    }

    public IEnumerator OnAunction1()
    {
        Debug.Log("[OnAunction1] Start:10s: " + userId);
        Debug.Log("[OnAunction1] Action:OnAunction | id: " + userId);
        yield return new WaitForSeconds(10);
        Debug.Log("[OnAunction1] 10s: " + userId);
        auctionActionFlag++;
    }
    public IEnumerator OnAunction2()
    {
        Debug.Log("[OnAunction2] Start:15s: " + userId);
        Debug.Log("[OnAunction2] Action:OnAunction | id: " + userId);
        yield return new WaitForSeconds(15);
        Debug.Log("[OnAunction2] 15s: " + userId);

        while (auctionActionFlag <= 0)
        {
            yield return new WaitForEndOfFrame();
        }
        turnBaseController.EndAction();
    }
}
