using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvitationPopupView : MonoBehaviour
{
    public delegate void OnButtonPressed();
    public OnButtonPressed OnEngageBtnPressed;
    public OnButtonPressed OnDeclineBtnPressed;

    public Animator animator;
    public TextMeshProUGUI MessageTXT;
    public Button EngageButton;
    public Button DeclineButton;

    public virtual void Init()
    {
        if(EngageButton != null)
        {
            EngageButton.onClick.RemoveAllListeners();
            EngageButton.onClick.AddListener(TriggerEngage);
        }

        if (DeclineButton != null)
        {
            DeclineButton.onClick.RemoveAllListeners();
            DeclineButton.onClick.AddListener(TriggerDecline);
        }            
    }

    public virtual void SetMessage(string _msg)
    {
        MessageTXT.text = _msg;
    }

    public virtual void TriggerEngage()
    {
        OnEngageBtnPressed?.Invoke();
    }

    public virtual void TriggerDecline()
    {
        OnDeclineBtnPressed?.Invoke();        
    }

    /// <summary>
    /// Play the Open popup anim
    /// </summary>
    public virtual void OpenPopup()
    {
        animator.SetTrigger(CONSTS.ANIM_POPUP_APPEAR_TR);
    }

    /// <summary>
    /// Play the Popup closing and deactive the popup after a bried delay.
    /// </summary>
    public virtual void ClosePopup()
    {
        animator.SetTrigger(CONSTS.ANIM_POPUP_DISAPPEAR_TR);
        StartCoroutine(DeactivePopup());
    }

    private IEnumerator DeactivePopup()
    {
        yield return new WaitForSeconds(CONSTS.ANIM_POPUP_SPEED);
        gameObject.SetActive(false);
    }
}
