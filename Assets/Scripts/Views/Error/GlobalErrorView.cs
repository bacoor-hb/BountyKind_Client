using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlobalErrorView : MonoBehaviour
{
    public delegate void OnEventTrigger();
    public OnEventTrigger OnCloseBtnPressed;

    [SerializeField]
    private Button CloseBtn;
    [SerializeField]
    private TextMeshProUGUI MessageTxt;
    [SerializeField]
    private GameObject CanvasRoot;

    /// <summary>
    /// Initialize the Error Popup
    /// </summary>
    public void Init()
    {
        CloseBtn.onClick.RemoveAllListeners();
        CloseBtn.onClick.AddListener(() => OnCloseBtnPressed?.Invoke());
    }

    public void ClosePopup()
    {
        CanvasRoot.SetActive(false);
    }

    /// <summary>
    /// Open the Error Popup with specific message
    /// </summary>
    /// <param name="msg">Message to show</param>
    public void OpenPopup(string msg)
    {
        CanvasRoot.SetActive(true);
    }
}