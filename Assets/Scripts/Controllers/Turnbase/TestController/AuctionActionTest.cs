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
        OnAuctionAction();
    }

    int auctionActionFlag = 0;
    public void OnAuctionAction()
    {
        auctionActionFlag = 0;
        StartCoroutine(OnAuction1());
        StartCoroutine(OnAuction2());
    }

    public IEnumerator OnAuction1()
    {
        Debug.Log("[OnAunction1] Action:OnAunction | id: " + userId);
        float cd = 5f;
        while (cd > 0)
        {
            yield return new WaitForSeconds(1);
            Debug.Log("[OnAunction1] Processing Data | id: " + userId);
            cd--;
        }
        Debug.Log("[OnAunction1] 5s: " + userId);
        auctionActionFlag++;
    }
    public IEnumerator OnAuction2()
    {
        Debug.Log("[OnAunction2] Action:OnAunction | id: " + userId);
        float cd = 3f;
        while (cd > 0)
        {
            yield return new WaitForSeconds(1);
            Debug.Log("[OnAunction2] Processing Data | id: " + userId);
            cd--;
        }
        Debug.Log("[OnAunction2] 3s: " + userId);

        while (auctionActionFlag <= 0)
        {
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("[OnAunction2] DONE: " + userId);
        turnBaseController.EndAction();
    }
}
