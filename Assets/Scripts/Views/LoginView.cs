using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : MonoBehaviour
{
    [Header ("Login Form")]
    [SerializeField]
    private GameObject LoginForm_Root;
    public Button Login_Btn;

    [Header ("User Data Form")]
    [SerializeField]
    private GameObject UserDataForm_Root;
    [SerializeField]
    private TextMeshProUGUI Address_Txt;
    [SerializeField]
    private TextMeshProUGUI Username_Txt;
    [SerializeField]
    private TextMeshProUGUI BalanceYU_Txt;
    [SerializeField]
    private TextMeshProUGUI BalanceYU2_Txt;
    [SerializeField]
    private TextMeshProUGUI BalanceFFE_Txt;
    [SerializeField]
    private TextMeshProUGUI BalanceRollTime_Txt;
    
    public Button Logout_Btn;
    public Button CreateRoom_Btn;

    /// <summary>
    /// Initialize the View.
    /// </summary>
    public void Init()
    {
        Login_Btn.onClick.RemoveAllListeners();
        Logout_Btn.onClick.RemoveAllListeners();
        CreateRoom_Btn.onClick.RemoveAllListeners();

        SetUserDataFormStatus(false);
        SetLoginFormStatus(true);
    }

    /// <summary>
    /// Set Active for Login Form
    /// </summary>
    /// <param name="state">false = Hide the popup</param>
    public void SetLoginFormStatus(bool state)
    {
        if(LoginForm_Root != null)
            LoginForm_Root.SetActive(state);
    }

    /// <summary>
    /// Set Active for User Data Form
    /// </summary>
    /// <param name="state">false = Hide the popup</param>
    public void SetUserDataFormStatus(bool state)
    {
        if (UserDataForm_Root != null)
            UserDataForm_Root.SetActive(state);
    }

    /// <summary>
    /// Fill all user Data on the form
    /// </summary>
    /// <param name="userData"></param>
    public void FillData(UserData userData)
    {
        Address_Txt.text = userData.address;
        Username_Txt.text = userData.username;
        BalanceYU_Txt.text = userData.tokenBalance.YU.ToString();
        BalanceYU2_Txt.text = userData.tokenBalance.YU2.ToString();
        BalanceFFE_Txt.text = userData.tokenBalance.FFE.ToString();
        BalanceRollTime_Txt.text = userData.rollNumber.ToString();
    }

    public void UpdateRollTime_Txt(int _newRollTime)
    {
        BalanceRollTime_Txt.text = _newRollTime.ToString();
    }
}
