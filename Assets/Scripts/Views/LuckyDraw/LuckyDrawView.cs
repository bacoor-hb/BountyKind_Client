using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LUCKYDRAW_POPUP
{
    ROLL,
    CONGRAT,
    INVITATION,
}

public class LuckyDrawView : InvitationPopupView
{
    public delegate void OnEventTrigger<T>(T data);
    public OnEventTrigger<int> OnLuckyDraw_EndDraw;

    [Header("Roll Popup")]
    [SerializeField]
    private AutoScrollUI RollUI;
    [Header("Congratulation Popup")]
    [SerializeField]
    private CongratView CongratView;
    [Header("Invitation Popup")]
    [SerializeField]
    private Transform InvitationPopup;

    #region Invitation Popup
    public override void Init()
    {
        base.Init();

        OnLuckyDraw_EndDraw = null;
        OnLuckyDraw_EndDraw += LuckyDraw_EndDraw;

        CloseAllPopup();
    }

    public override void SetMessage(string _msg)
    {
        Debug.Log("[LuckyDrawView] SetMessage: " + _msg);
        base.SetMessage(_msg);
    }

    public override void TriggerEngage()
    {
        Debug.Log("[LuckyDrawView] TriggerEngage");
        base.TriggerEngage();
    }

    public override void TriggerDecline()
    {
        Debug.Log("[LuckyDrawView] TriggerDecline");
        base.TriggerDecline();
    }
    #endregion

    public void LuckyDraw_EndDraw(int result)
    {
        Debug.Log("[LuckyDrawView] End Draw:" + result);
        RollUI.StopRolling(result);

        SwitchPopup(LUCKYDRAW_POPUP.CONGRAT);
        GameObject _itemPrefab;

        //CongratView.OpenPopup(_itemPrefab);
    }

    public void SwitchPopup(LUCKYDRAW_POPUP popup)
    {
        CloseAllPopup();
        switch (popup)
        {
            case LUCKYDRAW_POPUP.ROLL:
                RollUI.gameObject.SetActive(true);
                break;
            case LUCKYDRAW_POPUP.CONGRAT:
                CongratView.gameObject.SetActive(true);
                break;
            case LUCKYDRAW_POPUP.INVITATION:
                InvitationPopup.gameObject.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// Close All Lucky Draw Popup
    /// </summary>
    public void CloseAllPopup()
    {
        Debug.Log("[LuckyDrawView] CloseAllPopup...");
        RollUI.gameObject.SetActive(false);
        CongratView.gameObject.SetActive(false);    
        InvitationPopup.gameObject.SetActive(false);
    }
}
