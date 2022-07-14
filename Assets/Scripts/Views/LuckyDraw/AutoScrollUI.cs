using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class AutoScrollUI : MonoBehaviour
{
    public delegate void OnEventTrigger();
    public OnEventTrigger OnOpenPopupFinish;
    public OnEventTrigger OnRollFinish;
    public OnEventTrigger OnClosePopupFinish;

    [Header("Popup Component")]
    [SerializeField]
    private GameObject overlay;
    [SerializeField]
    private GameObject popupRoot;
    [SerializeField]
    private Animator animator;
    public Button RollBtn;

    [Header ("Setting")]
    [SerializeField]
    private ScrollSnapBase snapBase;
    [SerializeField]
    private UI_InfiniteScroll infUI;
    [SerializeField]
    [Tooltip("The rolling speed.")]
    private float ScrollSpeed = 15f;
    [SerializeField]
    [Tooltip("The rolling speed when stop running.")]
    private float StopSpeed = 7f;
    [SerializeField]
    [Tooltip("The minimum item count in the list")]
    private int MIN_ITEM = 10;   
    
    [SerializeField]
    [Tooltip ("The result when stop rolling")]
    private int result = 0;
    [Header("Input Data")]
    [SerializeField]
    private List<Sprite> rollingSprite;
    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private Transform itemContainer;

    private bool ToggleScroll;
    private int currentPage;

    //private void Start()
    //{
    //    Init();

    //    RollBtn.onClick.RemoveAllListeners();
    //    RollBtn.onClick.AddListener(() => Init(null));
    //}

    #region Popup Manage
    public void Init(List<Sprite> newItem = null)
    {
        if(newItem != null)
            rollingSprite = newItem;

        var childObj = snapBase.ChildObjects;
        if (childObj != null && childObj.Length > 0)
        {
            //for (int i = 0; i < childObj.Length; i++)
            //{
            //    Destroy(childObj[i]);
            //}
            return;
        }
        else
        {
            HorizontalLayoutGroup layout = itemContainer.GetComponent<HorizontalLayoutGroup>();
            layout.enabled = true;

            int totalItem = (rollingSprite.Count < MIN_ITEM) ? MIN_ITEM : rollingSprite.Count;
            for (int i = 0; i < totalItem; i++)
            {
                GameObject item = Instantiate(itemPrefab);
                item.transform.SetParent(itemContainer, false);

                Image image = item.GetComponent<Image>();
                image.sprite = rollingSprite[i % rollingSprite.Count];
            }

            //Initialize the infinite UI
            infUI.Init();
            snapBase.Init();            
        }
        snapBase.GoToScreen(0);

        RollBtn.onClick.RemoveAllListeners();
        RollBtn.onClick.AddListener(StartRolling);
    }

    public void Open_Popup(bool _instant = false)
    {
        currentPage = snapBase.CurrentPage;
        if (_instant)
        {
            overlay.SetActive(false);
            popupRoot.SetActive(true);
            OnOpenPopupFinish?.Invoke();
        }
        else
        {
            RollBtn.enabled = true;
            StartCoroutine(OpenPopup_Anim());
        }
    }

    /// <summary>
    /// Play the close popup anim, then close this popup
    /// </summary>
    public void ClosePopup(bool _instant)
    {
        if(!_instant)
        {
            overlay.SetActive(true);
            StartCoroutine(ClosePopup_Anim());
        }
        else
        {
            popupRoot.SetActive(false);
            OnClosePopupFinish?.Invoke();
        }
    }

    IEnumerator OpenPopup_Anim()
    {
        popupRoot.SetActive(true);
        animator.SetTrigger(CONSTS.ANIM_POPUP_APPEAR_TR);
        yield return new WaitForSeconds(CONSTS.ANIM_POPUP_SPEED);

        OnOpenPopupFinish?.Invoke();
        overlay.SetActive(false);
    }

    IEnumerator ClosePopup_Anim()
    {
        animator.SetTrigger(CONSTS.ANIM_POPUP_DISAPPEAR_TR);
        yield return new WaitForSeconds(CONSTS.ANIM_POPUP_SPEED);
        popupRoot.SetActive(false);

        OnClosePopupFinish?.Invoke();
    }
    #endregion
    /// <summary>
    /// Toggle the scroll for testing
    /// </summary>
    private void ToggleRolling()
    {
        ToggleScroll = !ToggleScroll;

        if (ToggleScroll)
        {
            snapBase.transitionSpeed = ScrollSpeed;
            StartCoroutine(StartAutoScroll());
        }
        else
        {
            snapBase.transitionSpeed = StopSpeed;
            StartCoroutine(StopAutoScrolling());
        }
    }

    /// <summary>
    /// Scroll to the next item in the list
    /// </summary>
    private void ChangePage()
    {
        snapBase.PreviousScreen();
        currentPage = snapBase.CurrentPage;
    }

    #region Scrolling Item Manage

    /// <summary>
    /// Trigger the Roll
    /// </summary>
    public void StartRolling()
    {
        if(!ToggleScroll)
        {
            ToggleScroll = true;
            RollBtn.enabled = false;
            snapBase.transitionSpeed = ScrollSpeed;
            StartCoroutine(StartAutoScroll());
        }        
    }

    /// <summary>
    /// Set the result of the roll
    /// </summary>
    /// <param name="_result"></param>
    public void StopRolling(int _result)
    {
        if (ToggleScroll)
        {
            ToggleScroll = false;
            Debug.Log("[AutoScrollUI] StopRolling: " + _result);
            result = _result;
            snapBase.transitionSpeed = StopSpeed;
            StartCoroutine(StopAutoScrolling());
        }
    }
    IEnumerator StartAutoScroll()
    {
        while (ToggleScroll)
        {
            ChangePage();
            yield return new WaitForUpdate();
        }
    }

    IEnumerator StopAutoScrolling()
    {
        if (!ToggleScroll)
        {
            while (currentPage != result)
            {
                ChangePage();
                yield return new WaitForUpdate();
            }
            OnRollFinish?.Invoke(); 
        }
    }
    #endregion
}
