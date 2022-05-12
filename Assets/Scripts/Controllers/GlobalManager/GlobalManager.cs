using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : GlobalSingleton<GlobalManager>
{
    [SerializeField]
    public LoadingManager LoadingManager { get; private set; }
    [SerializeField]
    public LanguageManager LanguageManager { get; private set; }
    [SerializeField]
    public WalletManager WalletManager { get; private set; }




    void Start()
    {
        LoadingManager = GetComponentInChildren<LoadingManager>();
        if(LoadingManager != null)
        {
            LoadingManager.LoadWithLoadingScene(SCENE_NAME.MainMenu);
        }
        else
        {
            Debug.LogError("Loading Manager cannot be found...");
        }
        LanguageManager = GetComponentInChildren<LanguageManager>();
        if (LanguageManager != null)
        {
            LanguageManager.languageView.SetCanvasStatus(false);
        }
        else
        {
            Debug.LogError("Language Manager cannot be found...");
        }

        Init();
    }

    private void Init()
    {
        
    }

    void Update()
    {
        
    }

}
