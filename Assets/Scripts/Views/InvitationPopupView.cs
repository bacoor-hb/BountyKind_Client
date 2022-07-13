using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvitationPopupView : MonoBehaviour
{
    public delegate void OnButtonPressed<T>(T data);
    public OnButtonPressed<bool> OnEngageBtnPressed;
    public delegate void OnEventTrigger();
    public OnEventTrigger OnOpenPopupFinish;
    public OnEventTrigger OnClosePopupFinish;

    [SerializeField]
    private GameObject rootCanvas;
    [SerializeField]
    private GameObject overlay;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private TextMeshProUGUI MessageTXT;

    public Button EngageButton;
    public Button DeclineButton;

    public virtual void Init()
    {
        if(EngageButton != null)
        {
            EngageButton.onClick.RemoveAllListeners();
            EngageButton.onClick.AddListener(() => TriggerEngage(false));
        }

        if (DeclineButton != null)
        {
            DeclineButton.onClick.RemoveAllListeners();
            DeclineButton.onClick.AddListener(() => TriggerEngage(true));
        }            
    }

    protected virtual void SetMessage(string _msg)
    {
        MessageTXT.text = _msg;
    }

    protected virtual void TriggerEngage(bool _skip)
    {
        OnEngageBtnPressed?.Invoke(_skip);
    }

    /// <summary>
    /// Play the Open popup anim
    /// </summary>
    public virtual void OpenPopup(bool _instant = false)
    {
        if(_instant)
        {
            rootCanvas.SetActive(true);            
            OnOpenPopupFinish?.Invoke();
        }
        else
        {
            overlay.SetActive(false);
            StartCoroutine(OpenPopup_Anim());
        }        
    }

    /// <summary>
    /// Play the Popup closing and deactive the popup after a bried delay.
    /// </summary>
    public virtual void ClosePopup(bool _instant = false)
    {
        if(_instant)
        {
            rootCanvas.SetActive(false);
            OnClosePopupFinish?.Invoke();
        }
        else
        {
            overlay.SetActive(true);
            StartCoroutine(ClosePopup_Anim());
        }        
    }

    /// <summary>
    /// Immediate open/close Popup without Animation
    /// </summary>
    /// <param name="state"></param>
    public virtual void SetPopup_State(bool state)
    {
        rootCanvas.SetActive(state);
        if(state)
        {
            OnOpenPopupFinish?.Invoke();
        }
        else
        {
            OnClosePopupFinish?.Invoke();
        }
    }

    IEnumerator OpenPopup_Anim()
    {
        rootCanvas.SetActive(true);
        animator.SetTrigger(CONSTS.ANIM_POPUP_APPEAR_TR);
        yield return new WaitForSeconds(CONSTS.ANIM_POPUP_SPEED);

        OnOpenPopupFinish?.Invoke();
    }

    IEnumerator ClosePopup_Anim()
    {
        animator.SetTrigger(CONSTS.ANIM_POPUP_DISAPPEAR_TR);
        yield return new WaitForSeconds(CONSTS.ANIM_POPUP_SPEED);
        rootCanvas.SetActive(false);

        OnClosePopupFinish?.Invoke();
    }
}
