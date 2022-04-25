using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class LanguageView : MonoBehaviour
{
    [SerializeField]
    private GameObject LanguageCanvas;
    [SerializeField]
    private TextMeshProUGUI LanguageText;
    [SerializeField]

private Button BtnEnglish;
    [SerializeField]

    private Button BtnJapanese;
    [SerializeField]

    private Button BtnClose;

    void Start()
    {
        
    }

    void Update()
    {
    }


    public void Init()
    {
        SetBtnEvent();
        UpdateUI();
    }

    

    public void SetCanvasStatus(bool status)
    {
        if (LanguageCanvas != null)
        {
            LanguageCanvas.SetActive(status);
        }
    }
    public void UpdateUI(){
        LanguageText.text = GlobalManager.Instance.languageManager.GetSentence(TEXT_UI.LANGUAGE);
        BtnEnglish.GetComponentInChildren<TextMeshProUGUI>().text=GlobalManager.Instance.languageManager.GetSentence(TEXT_UI.ENGLISH);
        BtnJapanese.GetComponentInChildren<TextMeshProUGUI>().text = GlobalManager.Instance.languageManager.GetSentence(TEXT_UI.JAPANESE);


    }
    public void SetBtnEvent(){
        BtnEnglish.onClick.RemoveAllListeners();
        BtnEnglish.onClick.AddListener(OnEnglish);
        BtnJapanese.onClick.RemoveAllListeners();
        BtnJapanese.onClick.AddListener(OnJapanese);
        BtnClose.onClick.RemoveAllListeners();
        BtnClose.onClick.AddListener(OnClose);
       
    }
    void OnEnglish(){
        GlobalManager.Instance.languageManager.SetLanguage(Language.English);
        UpdateUI();
    }
    void OnJapanese(){
        GlobalManager.Instance.languageManager.SetLanguage(Language.Japanese);
        UpdateUI();
    }
    void OnClose()
    {
        SetCanvasStatus(false);
    }
}
