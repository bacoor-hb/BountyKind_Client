using System.Collections;
using System.Collections.Generic;
using UI.ThreeDimensional;
using UnityEngine;
using UnityEngine.UI;

public class CongratView : MonoBehaviour
{
    public delegate void OnEventTrigger();
    public OnEventTrigger OnOpenPopupFinish;
    public OnEventTrigger OnClosePopupFinish;

    [Header ("Popup Component")]
    [SerializeField]
    private GameObject overlay;
    [SerializeField]
    private GameObject viewRoot;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Button CloseBtn;

    [Header ("3D Item Render")]
    [SerializeField]
    private UIObject3D ItemPrefab;
    [SerializeField]
    private UIObject3DImage ItemImg3D;
    public void Init(GameObject _itemPrefab = null)
    {
        if (_itemPrefab != null)
        {
            ItemPrefab.ObjectPrefab = _itemPrefab.transform;
            ItemImg3D.color = Color.white;
        }
        CloseBtn.onClick.RemoveAllListeners();
        CloseBtn.onClick.AddListener(() => ClosePopup());
    }

    /// <summary>
    /// Open the COngratulation popup with 3d Prize Show.
    /// </summary>
    /// <param name="_itemPrefab"></param>
    public void OpenPopup(bool _instant = false)
    {
        Debug.Log("[CongratView] OpenPopup...");

        if(_instant)
        {
            viewRoot.SetActive(true);
            overlay.SetActive(false);
            OnOpenPopupFinish?.Invoke();
        }
        else
        {
            overlay.SetActive(true);
            StartCoroutine(OpenPopup_Anim());
        }
        CloseBtn.onClick.RemoveAllListeners();
        CloseBtn.onClick.AddListener(() => ClosePopup(false));
    }

    /// <summary>
    /// Play the close popup anim, then close this popup
    /// </summary>
    public void ClosePopup(bool _instant = false)
    {
        if(_instant)
        {
            viewRoot.SetActive(false);
            OnClosePopupFinish?.Invoke();
        }
        else
        {
            overlay.SetActive(true);
            StartCoroutine(ClosePopup_Anim());
        }        
    }

    IEnumerator OpenPopup_Anim()
    {
        viewRoot.SetActive(true);
        animator.SetTrigger(CONSTS.ANIM_POPUP_APPEAR_TR);
        yield return new WaitForSeconds(CONSTS.ANIM_POPUP_SPEED);

        overlay.SetActive(false);
        OnOpenPopupFinish?.Invoke();
    }

    IEnumerator ClosePopup_Anim()
    {
        animator.SetTrigger(CONSTS.ANIM_POPUP_DISAPPEAR_TR);
        yield return new WaitForSeconds(CONSTS.ANIM_POPUP_SPEED);
        viewRoot.SetActive(false);

        OnClosePopupFinish?.Invoke();
    }
}
