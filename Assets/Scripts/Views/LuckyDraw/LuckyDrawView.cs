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

public class LuckyDrawView : MonoBehaviour
{
    public delegate void OnEventTrigger<T>(T data);
    public delegate void OnEventTrigger();
    public OnEventTrigger<bool> OnLuckyDraw_Engage;
    public OnEventTrigger<bool> OnLuckyDraw_CloseInvPopupEnd;
    public OnEventTrigger<bool> OnLuckyDraw_OpenInvPopupEnd;
    public OnEventTrigger<bool> OnLuckyDraw_CloseRollPopupEnd;
    public OnEventTrigger<bool> OnLuckyDraw_CloseCongratPopupEnd;

    public OnEventTrigger OnLuckyDraw_EndDraw;
    public OnEventTrigger<int> OnLuckyDraw_StartDraw;

    [Header("Roll Popup")]
    [SerializeField]
    private AutoScrollUI RollUI;
    [Header("Congratulation Popup")]
    [SerializeField]
    private CongratView CongratView;
    [Header("Invitation Popup")]
    [SerializeField]
    private InvitationPopupView InvitationPopup;

    [Header("Data Input")]
    [SerializeField]
    private List<Sprite> rollingSprite;
    [SerializeField]
    private GameObject winningObj;

    #region Init Popup
    /// <summary>
    /// Init the Engage Event
    /// </summary>
    public void Init_Phase1()
    {
        InvitationPopup.Init();

        InvitationPopup.OnEngageBtnPressed = null;
        InvitationPopup.OnEngageBtnPressed +=
            (engage) =>
            {
                OnLuckyDraw_Engage?.Invoke(engage);
            };

        
    }

    /// <summary>
    /// Init the Lucky draw UI
    /// </summary>
    /// <param name="_skip"></param>
    public void Init_Phase2(bool _skip)
    {
        if(_skip)
        {
            
        }
        else
        {
            RollUI.Init(rollingSprite);
            RollUI.RollBtn.onClick.AddListener(LuckyDraw_StartRolling);

            RollUI.OnClosePopupFinish = null;
            RollUI.OnClosePopupFinish += () => OnLuckyDraw_CloseRollPopupEnd?.Invoke(_skip);
        }

        InvitationPopup.OnClosePopupFinish = null;
        InvitationPopup.OnClosePopupFinish += () => OnLuckyDraw_CloseInvPopupEnd?.Invoke(_skip);        
    }

    public void Init_Phase3()
    {
        CongratView.Init(winningObj);

        CongratView.OnClosePopupFinish = null;
        CongratView.OnClosePopupFinish += () => OnLuckyDraw_CloseCongratPopupEnd?.Invoke(true);
    }
    #endregion  

    public void LuckyDraw_StartRolling()
    {
        OnLuckyDraw_StartDraw?.Invoke(0);
    }

    /// <summary>
    /// Trigger end rolling in the Lucky Draw popup
    /// </summary>
    /// <param name="result"></param>
    public void LuckyDraw_EndDraw(int result)
    {
        Debug.Log("[LuckyDrawView] End Draw:" + result);
        RollUI.StopRolling(result);

        StartCoroutine(WaitForEndDraw());
    }

    IEnumerator WaitForEndDraw()
    {
        yield return new WaitForSeconds(CONSTS.ANIM_DRAW_SPEED);
        OnLuckyDraw_EndDraw?.Invoke();
    }

    public void OpenPopup(LUCKYDRAW_POPUP popup, bool _instant = false)
    {
        //CloseAllPopup();
        switch (popup)
        {
            case LUCKYDRAW_POPUP.ROLL:
                RollUI.Open_Popup(_instant);
                break;
            case LUCKYDRAW_POPUP.CONGRAT:
                CongratView.OpenPopup(_instant);
                break;
            case LUCKYDRAW_POPUP.INVITATION:
                InvitationPopup.OpenPopup(_instant);
                break;
        }
    }

    /// <summary>
    /// Close All Lucky Draw Popup
    /// </summary>
    public void CloseAllPopup()
    {
        Debug.Log("[LuckyDrawView] CloseAllPopup...");
        RollUI.ClosePopup(true);
        CongratView.ClosePopup(true);
        InvitationPopup.ClosePopup(true);
    }

    /// <summary>
    /// Close specific popup
    /// </summary>
    /// <param name="popup">Popup to close</param>
    /// <param name="_instant">instant = false: Play Close Popup animation</param>
    public void ClosePopup(LUCKYDRAW_POPUP popup, bool _instant = false)
    {
        switch (popup)
        {
            case LUCKYDRAW_POPUP.ROLL:
                RollUI.ClosePopup(_instant);
                break;
            case LUCKYDRAW_POPUP.CONGRAT:
                CongratView.ClosePopup(_instant);
                break;
            case LUCKYDRAW_POPUP.INVITATION:
                InvitationPopup.ClosePopup(_instant);
                break;
        }
    }
}