using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalGameView : MonoBehaviour
{
    [Header("Action Button")]
    public Button RollDice_Btn;
    public Button Move_Btn;
    public Button EndTurn_Btn;
    public Button Exit_Btn;

    /// <summary>
    /// Reset all Button Event and Clear all the Text
    /// </summary>
    public void Init()
    {
        RollDice_Btn.onClick.RemoveAllListeners();
        Move_Btn.onClick.RemoveAllListeners();
        EndTurn_Btn.onClick.RemoveAllListeners();
        Exit_Btn.onClick.RemoveAllListeners();

        SetBtn_State(ACTION_TYPE.ROLL_DICE, true);
        SetBtn_State(ACTION_TYPE.MOVE, false);
        SetBtn_State(ACTION_TYPE.END_TURN, true);
    }

    public void SetBtn_State(ACTION_TYPE actionType, bool _state)
    {
        switch (actionType)
        {
            case ACTION_TYPE.MOVE:
                Move_Btn.interactable = _state;
                break;
            case ACTION_TYPE.ROLL_DICE:
                RollDice_Btn.interactable = _state;
                break;
            case ACTION_TYPE.END_TURN:
                EndTurn_Btn.interactable = _state;
                break;
            default:
                break;
        }
    }
}
