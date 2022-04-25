using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private MenuView MenuView;
    void Start()
    {

        if (MenuView != null)
        {
            MenuView.Init();
            MenuView.OnStartGameClicked = null;
            MenuView.OnStartGameClicked += OnStart;        
        }
    }

    void Update()
    {
        
    }

    void OnStart()
    {
        SceneManager.LoadScene("Scenes/Test_Scene/Loading_Scene");
    }
}
