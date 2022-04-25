using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : Singleton<GlobalManager>
{
    public LoadingManager loadingManager { get; private set; }
    public LanguageManager languageManager { get; private set; }




    void Start()
    {
        loadingManager = GetComponentInChildren<LoadingManager>();
        if(loadingManager != null)
        {
            loadingManager.LoadWithLoadingScene(SCENE_NAME.Menu_Scene);
        }
        else
        {
            Debug.LogError("Loading Manager cannot be found...");
        }
        languageManager = GetComponentInChildren<LanguageManager>();
        if (languageManager != null)
        {
            languageManager.languageView.SetCanvasStatus(false);
        }
        else
        {
            Debug.LogError("Language Manager cannot be found...");
        }
    }

    void Update()
    {
        
    }

}
