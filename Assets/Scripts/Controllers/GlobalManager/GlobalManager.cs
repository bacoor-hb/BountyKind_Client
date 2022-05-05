using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalManager : GlobalSingleton<GlobalManager>
{
    [SerializeField]
    private GameObject JoinLobbyBTN;
    public LoadingManager LoadingManager { get; private set; }
    public LanguageManager LanguageManager { get; private set; }

    public BountyColyseusManager NetworkManager { get; private set; }



    void Start()
    {
        LoadingManager = GetComponentInChildren<LoadingManager>();
        if(LoadingManager != null)
        {
            LoadingManager.LoadWithLoadingScene(SCENE_NAME.Menu_Scene);
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
        NetworkManager = GetComponentInChildren<BountyColyseusManager>();
        if (NetworkManager != null)
        {
            JoinLobbyBTN.SetActive(true);
            JoinLobbyBTN.GetComponent<Button>().onClick.AddListener(() => {
                NetworkManager.Connect();
                NetworkManager.JoinLobby();
                SceneManager.LoadScene("Test_Socket_Lobby");
            });
        }
        else
        {
            Debug.LogError("Network Manager cannot be found...");
        }
    }

    void Update()
    {
        
    }

}
