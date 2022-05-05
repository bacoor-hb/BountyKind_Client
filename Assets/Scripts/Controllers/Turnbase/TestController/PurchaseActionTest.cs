using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseActionTest : Action
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
        StartCoroutine(OnPurchase());
    }

    private IEnumerator OnPurchase()
    {
        Debug.Log("[OnPurchase] Waiting for Data | id: " + userId);
        float cd = 3f;
        while (cd > 0)
        {
            yield return new WaitForSeconds(1);
            Debug.Log("[OnPurchase] Processing Data | id: " + userId);
            cd--;
        }        
        Debug.Log("[OnPurchase] Get data success | id: " + userId);
        turnBaseController.EndAction();
    }
}
