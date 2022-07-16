using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalGameView : MonoBehaviour
{
    [Header("Feature Popup")]
    public LuckyDrawView LuckyDrawPopup;
    public CombatGameView CombatGameView;
    public ChanceView ChanceView;
    public RollDiceView RollDiceView;

    [Header("User Data")]
    public TextMeshProUGUI userNameTxt;
    public TextMeshProUGUI userAddressTxt;
    public TextMeshProUGUI energyTxt;
    public TextMeshProUGUI energyEarnedTxt;
    public TextMeshProUGUI yuPointTxt;
    public TextMeshProUGUI yuEarnedTxt;

    [Header("Other Button")]
    public Button QuitBoard_Btn;

    [SerializeField]
    private GameObject canvasRoot;
    /// <summary>
    /// Reset all Button Event and Clear all the Text
    /// </summary>
    public void Init()
    {
        CombatGameView.Init();
        ChanceView.Init();
        RollDiceView.Init();
        LuckyDrawPopup.Init_Phase1();
    }

    public void SetState_RollDicePopup(bool _state)
    {
        Debug.Log("[LocalGameView] Open_RollDicePopup: " + _state);
        if (_state)
        {
            RollDiceView.OpenPopup();
        }
        else
        {
            RollDiceView.ClosePopup();
        }
    }

    public void SetState_LuckyDrawPopup(bool _state)
    {
        Debug.Log("[LocalGameView] Open_LuckyDrawPopup");

        if (_state)
        {
            LuckyDrawPopup.OpenPopup(LUCKYDRAW_POPUP.INVITATION);
        }
        else
        {
            LuckyDrawPopup.CloseAllPopup();
        }
    }

    public void SetState_ChanceView(bool _state)
    {
        Debug.Log("[LocalGameView] Open_ChanceView");
        if (_state)
        {
            ChanceView.OpenPopup();
        }
        else
        {
            ChanceView.ClosePopup();
        }
    }

    public void SetState_BattleView(bool _state)
    {
        Debug.Log("[LocalGameView] Open_BattleView");
        if (_state)
        {
            CombatGameView.OpenPopup();
        }
        else
        {
            CombatGameView.ClosePopup();
        }
    }

    /// <summary>
    /// Deactive all Actions button on the View
    /// </summary>
    public void DeactiveAllPopup()
    {
        LuckyDrawPopup.CloseAllPopup();
        CombatGameView.SetPopup_State(false);
        ChanceView.SetPopup_State(false);
        RollDiceView.SetPopup_State(false);
    }

    public void SetCanvasRootState(bool state)
    {
        canvasRoot.SetActive(state);
    }

    /// <summary>
    /// Update the User UI
    /// </summary>
    /// <param name="userData"></param>
    public void UpdateUserData(UserData userData)
    {
        userNameTxt.text = STRING_EXT.STRING_FORMAT(userData.username);
        userAddressTxt.text = STRING_EXT.STRING_FORMAT(userData.address);

        UpdateBalance(userData.tokenBalance);
    }

    /// <summary>
    /// Update the Balance UI
    /// </summary>
    /// <param name="tokenBalance"></param>
    public void UpdateBalance(TokenBalance tokenBalance)
    {
        energyTxt.text = STRING_EXT.NUMBER_FORMAT_DOT(tokenBalance.energy);
        yuPointTxt.text = STRING_EXT.NUMBER_FORMAT_DOT(tokenBalance.yuPoint);
        energyEarnedTxt.text = STRING_EXT.NUMBER_FORMAT_DOT(tokenBalance.energyEarned);
        yuEarnedTxt.text = STRING_EXT.NUMBER_FORMAT_DOT(tokenBalance.yuEarned);
    }
}
