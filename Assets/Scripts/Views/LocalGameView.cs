using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalGameView : MonoBehaviour
{
    [Header("Action Popup")]
    public Button RollDice_Btn;
    public Button Move_Btn;
    public Button LuckyDraw_Btn;
    public Button Combat_Btn;
    public Button Chance_Btn;
    public Button EndTurn_Btn;
    public Button Exit_Btn;

    [Header ("Feature Popup")]
    public LuckyDrawView LuckyDrawPopup;
    public CombatGameView CombatGameView;
    public ChanceView ChanceView;
    public RollDiceView RollDiceView;

    [Header("User Data")]
    public TextMeshProUGUI userNameTxt;
    public TextMeshProUGUI userAddressTxt;
    public TextMeshProUGUI energyTxt;
    public TextMeshProUGUI yuPointTxt;

    /// <summary>
    /// Reset all Button Event and Clear all the Text
    /// </summary>
    public void Init()
    {
        LuckyDrawPopup.Init();
        CombatGameView.Init();
        ChanceView.Init();
        RollDiceView.Init();

        DeactiveAllPopup();
    }

    /// <summary>
    /// Switch the Button State
    /// </summary>
    /// <param name="actionType">The Button to be interract with</param>
    /// <param name="_state">true: enable the Button</param>
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
            case ACTION_TYPE.LUCKY_DRAW:
                LuckyDraw_Btn.interactable = _state;
                break;
            case ACTION_TYPE.CHANCE:
                Chance_Btn.interactable = _state;
                break;
            case ACTION_TYPE.COMBAT:
                Combat_Btn.interactable = _state;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Deactive all Actions button on the View
    /// </summary>
    public void DeactiveAllPopup()
    {
        SetBtn_State(ACTION_TYPE.ROLL_DICE, false);
        SetBtn_State(ACTION_TYPE.MOVE, false);
        SetBtn_State(ACTION_TYPE.CHANCE, false);
        SetBtn_State(ACTION_TYPE.COMBAT, false);
        SetBtn_State(ACTION_TYPE.LUCKY_DRAW, false);
        SetBtn_State(ACTION_TYPE.END_TURN, false);
    }

    public void UpdateUserData(UserData userData)
    {
        userNameTxt.text = STRING_EXT.STRING_FORMAT(userData.username);
        userAddressTxt.text = STRING_EXT.STRING_FORMAT(userData.address);

        UpdateBalance(userData.tokenBalance);
    }

    public void UpdateBalance(TokenBalance tokenBalance)
    {
        energyTxt.text = STRING_EXT.NUMBER_FORMAT_DOT(tokenBalance.energy);
        yuPointTxt.text = STRING_EXT.NUMBER_FORMAT_DOT(tokenBalance.yuPoint);
    }
}
