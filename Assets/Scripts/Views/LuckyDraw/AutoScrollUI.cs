using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class AutoScrollUI : MonoBehaviour
{
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
    
    public Button RollBtn;
    
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

    public void Init(List<Sprite> newItem = null)
    {
        if(newItem != null)
            rollingSprite = newItem;

        var childObj = itemContainer.GetComponentsInChildren<Transform>();
        if(childObj != null)
        {
            for(int i = 1; i < childObj.Length; i++)
            {
                Destroy(childObj[i].gameObject);
            }
        }
      
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

        Open_Popup();
    }

    public void Open_Popup()
    {
        currentPage = snapBase.CurrentPage;

        RollBtn.enabled = true;
        RollBtn.onClick.RemoveAllListeners();
        RollBtn.onClick.AddListener(StartRolling);
    }

    /// <summary>
    /// Toggle the scroll for testing
    /// </summary>
    private void ToggleRolling()
    {
        ToggleScroll = !ToggleScroll;

        if(ToggleScroll)
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
        }
    }

    private void ChangePage()
    {
        snapBase.PreviousScreen();
        currentPage = snapBase.CurrentPage;
    }

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
        if (!ToggleScroll)
        {
            result = _result;
            snapBase.transitionSpeed = StopSpeed;
            StartCoroutine(StopAutoScrolling());
        }
    }
}
