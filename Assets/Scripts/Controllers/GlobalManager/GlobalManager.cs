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
    public UserDataManager UserDataManager { get; private set; }
    [SerializeField]
    public MapNodeDataManager MapNodeDataManager { get; private set; }
    [SerializeField]
    public NetworkManager NetworkManager { get; private set; }

    void Start()
    {
        Init();

        if (LoadingManager != null)
        {
            LoadingManager.LoadWithLoadingScene(SCENE_NAME.MainMenu);
        }
        else
        {
            Debug.LogError("Loading Manager cannot be found...");
        }
    }

    private void Init()
    {
        //Get all Manager... 
        LoadingManager = GetComponentInChildren<LoadingManager>();
        //LanguageManager = GetComponentInChildren<LanguageManager>();
        UserDataManager = GetComponentInChildren<UserDataManager>();
        MapNodeDataManager = GetComponentInChildren<MapNodeDataManager>();
        NetworkManager = GetComponentInChildren<NetworkManager>();

        //Init all Manager...
        LoadingManager.Init();
        UserDataManager.Init();
        MapNodeDataManager.Init();
        NetworkManager.Init();
        //LanguageManager.Init();
    }

    void Update()
    {

    }

    public void Logout()
    {
        NetworkManager.Disconnect();
    }

}
