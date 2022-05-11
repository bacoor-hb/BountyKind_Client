using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : IPlayer
{
    public delegate void OnEventTriggered<T>(T data);
    public OnEventTriggered<int> OnStartRollingDice;
    public OnEventTriggered<int> OnEndRollingDice;
    public OnEventTriggered<int> OnStartMoving;
    public OnEventTriggered<int> OnEndMoving;
    public User UserData { get; private set; }
    [SerializeField]
    private WalletManager WalletManager;

    [Header ("Turn Action")]
    [SerializeField]
    private Action EndTurnAction;
    [SerializeField]
    private Action RollDiceAction;
    [SerializeField]
    private Action MoveAction;

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

        EndTurnAction.InitAction(id, _controller);

        RollDiceAction.InitAction(id, _controller);
        RollDiceAction.StartAction += StartRollDice;
        RollDiceAction.EndAction += EndRollDice;

        MoveAction.InitAction(id, _controller);
        MoveAction.StartAction += StartMoving;
        MoveAction.EndAction += EndMoving;
        


        WalletManager.Init(UserData);
        WalletManager.OnUpdateMoney += OnYu2_UpdateEvent;

        myMaterial = GetComponent<Renderer>().material;
    }
    #endregion

    #region Turn Management
    public Action GetAction(ACTION_TYPE actionType)
    {
        return actionType switch
        {
            ACTION_TYPE.MOVE => MoveAction,
            ACTION_TYPE.ROLL_DICE => RollDiceAction,
            ACTION_TYPE.END_TURN => EndTurnAction,
            _ => null,
        };
    }
    #endregion

    #region Wallet Value
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

    public WalletData GetWalletData()
    {
        return WalletManager.walletData;
    }
    #endregion

    #region Event Action
    private void StartRollDice()
    {
        OnStartRollingDice?.Invoke(id);
    }

    private void EndRollDice()
    {
        OnEndRollingDice?.Invoke(id);
    }

    private void StartMoving()
    {
        OnStartMoving?.Invoke(id);
    }

    private void EndMoving()
    {
        OnEndMoving?.Invoke(id);
    }
    #endregion
}
