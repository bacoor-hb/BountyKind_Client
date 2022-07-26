using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FormationViewManager : MonoBehaviour
{
    [Header("View Manager")]
    public ButtonViewManager buttonViewManager;
    public ScrollViewManager scrollViewManager;
    public BoardViewManager boardViewManager;
    public CurrentUnitView currentUnitViewManager;

    [Header("Popup Element")]
    public GameObject notifyPopup;
    [SerializeField]
    public GameObject canvasRoot;
    TextMeshProUGUI notifyTxt;
    public void Init()
    {
        buttonViewManager.Init();
        scrollViewManager.Init();
        boardViewManager.Init();

        notifyTxt = notifyPopup.GetComponentInChildren<TextMeshProUGUI>();
        SetFormationCanvasState(false);
    }

    /// <summary>
    /// Open/Close formation Canvas.
    /// </summary>
    /// <param name="state">true: Open Canvas/ false: Close Canvas</param>
    public void SetFormationCanvasState(bool state)
    {
        if (canvasRoot != null)
        {
            canvasRoot.SetActive(state);
        }
        else
        {
            Debug.LogError("[FormationViewManager] SetFormationCanvasState ERROR: Canvas is null.");
        }
    }

    /// <summary>
    /// Set formation view state
    /// </summary>
    /// <param name="type">
    /// type 0: main menu screen formation
    /// type 1: game screen formation
    /// </param>
    public void SetFormationViewState(FORMATION_VIEW_STYLE type)
    {
        switch (type)
        {
            case FORMATION_VIEW_STYLE.LOBBY:
                buttonViewManager.BackButton.interactable = true;
                buttonViewManager.ResetFormationButton.interactable = true;
                buttonViewManager.RemoveCharacterButton.interactable = true;
                scrollViewManager.gameObject.SetActive(true);
                break;
            case FORMATION_VIEW_STYLE.BOARD:
                buttonViewManager.BackButton.interactable = false;
                buttonViewManager.ResetFormationButton.interactable = false;
                buttonViewManager.RemoveCharacterButton.interactable = false;
                scrollViewManager.gameObject.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// Open Notify popup, then fade out after several time
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public IEnumerator SetPopupViewState(FormationViewState state, float fadeTime = 1.0f)
    {
        notifyPopup.SetActive(true);

        switch (state)
        {
            case FormationViewState.SET_FORMATION_SUCCESS:
                notifyTxt.text = "Set formation success";
                break;
            case FormationViewState.SET_FORMATION_FAILED:
                notifyTxt.text = "Set formation failed";
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(fadeTime);
        notifyTxt.text = "";
        notifyPopup.SetActive(false);
    }
}

public enum FormationViewState
{
    SET_FORMATION_SUCCESS,
    SET_FORMATION_FAILED,
}

public enum FORMATION_VIEW_STYLE
{
    LOBBY,
    BOARD
}
