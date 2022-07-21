using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationViewManager : MonoBehaviour
{
    // Start is called before the first frame update\
    public ButtonViewManager buttonViewManager;
    public ScrollViewManager scrollViewManager;
    public BoardViewManager boardViewManager;
    public void Init()
    {
        buttonViewManager.Init();
        scrollViewManager.Init();
        boardViewManager.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Set formation view state
    /// </summary>
    /// <param name="type">
    /// type 0: main menu screen formation
    /// type 1: game screen formation
    /// </param>
    public void SetFormationViewState(int type)
    {
        if (type == 0)
        {
            buttonViewManager.ResetFormationButton.interactable = true;
            buttonViewManager.RemoveCharacterButton.interactable = true;
            scrollViewManager.gameObject.SetActive(true);
        }
        else
        {
            buttonViewManager.ResetFormationButton.interactable = false;
            buttonViewManager.RemoveCharacterButton.interactable = false;
            scrollViewManager.gameObject.SetActive(false);
        }
    }
}
