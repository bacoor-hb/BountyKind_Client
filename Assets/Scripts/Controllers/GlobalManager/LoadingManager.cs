using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    private LoadingView LoadingView;

    SCENE_NAME targetScene;

    void Start()
    {
        
    }

    /// <summary>
    /// Only use by Local Loading Scene
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public IEnumerator LoadScene_Async(SCENE_NAME sceneName)
    {
        LoadingView.SetCanvasStatus(true);
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName.ToString());
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            Debug.Log("ProgressB :" + asyncOperation.progress);
            LoadingView.IncrementProgess(asyncOperation.progress);
            LoadingView.SetMessage(asyncOperation.progress * 100 + "%");
            if (asyncOperation.progress >= 0.9f)
            {
                LoadingView.SetMessage("Press the space bar to continue...");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    asyncOperation.allowSceneActivation = true;
                    LoadingView.SetCanvasStatus(false);
                }                    
            }

            yield return null;
        }
    }

    /// <summary>
    /// Load a Scene via the Loading Scene.
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadWithLoadingScene(SCENE_NAME sceneName)
    {
        PlayerPrefs.SetString(CONSTS.SCENE_KEY, sceneName.ToString());

        StartCoroutine(LoadScene_Async(SCENE_NAME.Loading_Scene));
    }
}
