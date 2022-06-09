using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    private Queue<LoadingAction> actionsQueue;
    [SerializeField]
    private LoadingView LoadingView;
    [HideInInspector]
    public SCENE_NAME targetScene;

    bool onLoadingAction;
    public void Init()
    {
        targetScene = SCENE_NAME.MainMenu;
        actionsQueue = new Queue<LoadingAction>();
        onLoadingAction = false;
    }

    /// <summary>
    /// Only use by Local Loading Scene
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public IEnumerator LoadScene_Async(SCENE_NAME sceneName)
    {
        LoadingView.SetCanvasStatus(true);
        yield return new WaitForEndOfFrame();

        ActionProcess();

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName.ToString());
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            //Debug.Log("[LoadScene_Async] ProgressB :" + asyncOperation.progress);
            LoadingView.IncrementProgess(asyncOperation.progress);
            LoadingView.SetMessage(asyncOperation.progress * 100 + "%");
            if (asyncOperation.progress >= 0.9f
                && onLoadingAction)
            { 
                asyncOperation.allowSceneActivation = true;
                LoadingView.SetCanvasStatus(false);
            }
            
            yield return new WaitForSeconds(0.5f);
        }
    }

    /// <summary>
    /// Load a Scene via the Loading Scene.
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadWithLoadingScene(SCENE_NAME sceneName)
    {
        targetScene = sceneName;

        Debug.Log("[LoadingManager] Load With LoadingScene: " + sceneName.ToString());
        SceneManager.LoadScene(SCENE_NAME.LoadingScene.ToString());
        //StartCoroutine(LoadScene_Async(SCENE_NAME.LoadingScene));
    }

    public void AddLoadingAction(LoadingAction loadingAction)
    {
        actionsQueue.Enqueue(loadingAction);
    }

    /// <summary>
    /// Process the last action in the Queue.
    /// </summary>
    private void ActionProcess()
    {
        if(actionsQueue.Count > 0)
        {
            onLoadingAction = false;
            LoadingAction tmpAction = actionsQueue.Dequeue();
            tmpAction.OnEndLoadingAction = null;
            tmpAction.OnEndLoadingAction += ActionProcess;

            tmpAction.StartAction();
        }
        else
        {
            onLoadingAction = true;
        }
    }
}
