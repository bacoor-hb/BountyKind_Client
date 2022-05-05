using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuView : MonoBehaviour
{
    public delegate void OnButtonCliked();
    public OnButtonCliked OnStartGameClicked;

    [SerializeField]
    private Button startGameBtn;
    [SerializeField]
    private Button btnLanguage;
    [SerializeField]
    private TextMeshProUGUI MsgTxt;

    public void Init()
    {
        SetStartBtnEvent();
        setLanguageEvent();

    }

    void Update()
    {
        
    }

    void SetStartBtnEvent()
    {
        startGameBtn.onClick.RemoveAllListeners();
        startGameBtn.onClick.AddListener(OnLogin);
    }
    void setLanguageEvent()
    {
        btnLanguage.onClick.RemoveAllListeners();
        btnLanguage.onClick.AddListener(OnShowLanguageView);
    }
    void OnLogin()
    {
        Debug.Log("Clicked on start game Button.");
        OnStartGameClicked?.Invoke();
        StartCoroutine(OnStartGameDelay());
    }
    void OnShowLanguageView()
    {
        //show language view

        GlobalManager.Instance.LanguageManager.languageView.SetCanvasStatus(true);
    }

    IEnumerator OnStartGameDelay()
    {
        startGameBtn.interactable = false;
        yield return new WaitForSeconds(5.0f);
        startGameBtn.interactable = true;
    }

    public void SetMessage(string _msg)
    {
        MsgTxt.text = _msg;
    }
}
