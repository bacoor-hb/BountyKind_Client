using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalViewManager : MonoBehaviour
{
    public QueueViewManager queueViewManager;
    public FightSceneButtonViewManager buttonViewManager;
    public FightSceneBoardViewManager boardViewManager;
    public CurrentUnitView currentUnitViewManager;
    public ResultViewManager resultViewManager;
    public TurnViewManager turnViewManager;
    [SerializeField]
    private GameObject canvasRoot;
    // Start is called before the first frame update
    public void Init()
    {
        boardViewManager.Init();
    }

    void Update()
    {

    }

    public void SetViewState(bool state)
    {
        canvasRoot.SetActive(state);
    }
}
