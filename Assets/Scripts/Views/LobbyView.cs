using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyView : MonoBehaviour
{
    [Header("Login Form")]
    [SerializeField]
    private GameObject LoginForm_Root;
    public Button Login_Btn;

    [Header("User Data Area")]
    [SerializeField]
    private GameObject LobbyForm_Root;
    [SerializeField]
    private TextMeshProUGUI Address_Txt;
    [SerializeField]
    private TextMeshProUGUI Username_Txt;
    [SerializeField]
    private TextMeshProUGUI BalanceYuPoint_Txt;
    [SerializeField]
    private TextMeshProUGUI BalanceEnergy_Txt;

    [Header("Room Area")]
    public TMP_Dropdown RoomType_DD;
    public Button Logout_Btn;
    public Button CreateRoom_Btn;
    public Button Formation_Btn;

    /// <summary>
    /// Initialize the View.
    /// </summary>
    public void Init()
    {
        Login_Btn.onClick.RemoveAllListeners();
        Logout_Btn.onClick.RemoveAllListeners();
        CreateRoom_Btn.onClick.RemoveAllListeners();
        Formation_Btn.onClick.RemoveAllListeners();

        SetLoobyFormStatus(false);
        SetLoginFormStatus(true);
    }

    /// <summary>
    /// Set Active for Login Form
    /// </summary>
    /// <param name="state">false = Hide the popup</param>
    public void SetLoginFormStatus(bool state)
    {
        if (LoginForm_Root != null)
            LoginForm_Root.SetActive(state);
    }

    /// <summary>
    /// Set Active for User Data Form
    /// </summary>
    /// <param name="state">false = Hide the popup</param>
    public void SetLoobyFormStatus(bool state)
    {
        if (LobbyForm_Root != null)
            LobbyForm_Root.SetActive(state);
    }

    /// <summary>
    /// Fill all user Data on the form
    /// </summary>
    /// <param name="userData"></param>
    public void FillData(UserData userData)
    {
        Address_Txt.text = STRING_EXT.STRING_FORMAT(userData.address);
        Username_Txt.text = STRING_EXT.STRING_FORMAT(userData.username);
        BalanceYuPoint_Txt.text = STRING_EXT.NUMBER_FORMAT_DOT(userData.tokenBalance.yuPoint);
        BalanceEnergy_Txt.text = STRING_EXT.NUMBER_FORMAT_DOT(userData.tokenBalance.energy);
    }

    public void UpdateEnergy_Txt(int _newEnergyValue)
    {
        BalanceEnergy_Txt.text = STRING_EXT.NUMBER_FORMAT_DOT(_newEnergyValue);
    }

    /// <summary>
    /// Update the Room Type Drop down List
    /// </summary>
    /// <param name="_mapList"></param>
    public void UpdateMapList(List<MapShort_MSG> _mapList)
    {
        RoomType_DD.ClearOptions();

        for (var i = 0; i < _mapList.Count; i++)
        {
            TMP_Dropdown.OptionData newOption = new TMP_Dropdown.OptionData
            {
                text = _mapList[i].name
            };
            RoomType_DD.options.Add(newOption);
        }

        RoomType_DD.captionText.text = RoomType_DD.options[0].text;
    }
}
