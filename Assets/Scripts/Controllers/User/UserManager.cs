using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : IPlayer
{
    public delegate void OnEventTriggered<T>(T data);

    public string userToken;
    public User UserData { get; private set; }
    [SerializeField]
    private WalletManager WalletManager;

    private TurnBaseController TurnBaseController;
    Material myMaterial;
    #region Initialize
    /// <summary>
    /// Initializing the User data and Wallet
    /// </summary>
    /// <param name="user"></param>
    public void Init(User user, TurnBaseController _controller)
    {
        UserData = user;
        TurnBaseController = _controller;
        WalletManager.Init(UserData);

        WalletManager.OnUpdateMoney += OnYu2_UpdateEvent;

        myMaterial = GetComponent<Renderer>().material;
    }
    #endregion

    #region Wallet Value
    /// <summary>
    /// Check the current dice number
    /// </summary>
    /// <param name="diceTime"></param>
    /// <returns></returns>
    public bool AllowRollDice(int diceTime)
    {
        long total = WalletManager.walletData.TotalDice + diceTime;
        Debug.Log("[UserManager] [AllowRollDice] total dice Time : " + total);
        return total > 0;
    }

    /// <summary>
    /// Update the Yu2 value
    /// </summary>
    /// <param name="amount"></param>
    public void Yu2_UpdateValue(long amount)
    {
        WalletManager.OnYu2_Update(amount);
    }
    private void OnYu2_UpdateEvent(long money)
    {
        Debug.Log("[UserManager] [OnChangeMoneyEvent] Money = " + money);
    }

    public override void StartTurn()
    {        
        myMaterial.color = Color.red;
        Debug.Log("StartTurn: id: " + id);
    }

    public override void EndTurn()
    {
        myMaterial.color = Color.white;
        Debug.Log("EndTurn: id: " + id);
    }
    #endregion
}
