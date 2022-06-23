using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserActionLocalManager : IPlayer
{
    public delegate void OnEventTriggered<T>(T data);
    public OnEventTriggered<int> OnStartRollingDice;
    public OnEventTriggered<int> OnEndRollingDice;
    public OnEventTriggered<int> OnStartMoving;
    public OnEventTriggered<int> OnEndMoving;
    public OnEventTriggered<int> OnStartLuckyDraw;
    public OnEventTriggered<int> OnEndLuckyDraw;
    public OnEventTriggered<int> OnStartChance;
    public OnEventTriggered<int> OnEndChance;
    public OnEventTriggered<int> OnStartCombat;
    public OnEventTriggered<int> OnEndCombat;

    [Header ("Turn Action")]
    [SerializeField]
    private Action EndTurnAction;
    [SerializeField]
    private Action RollDiceAction;
    [SerializeField]
    private Action MoveAction;
    [SerializeField]
    private Action LuckyDrawAction;
    [SerializeField]
    private Action ChanceAction;
    [SerializeField]
    private Action CombatAction;

    private TurnBaseController TurnBaseController;

    [Header ("Only for TEST")]
    [SerializeField]
    private Material myMaterial;

    #region Initialize
    /// <summary>
    /// Initializing the User Action
    /// </summary>
    /// <param name="user"></param>
    public void Init(TurnBaseController _controller)
    {
        TurnBaseController = _controller;

        EndTurnAction.InitAction(id, _controller);

        RollDiceAction.InitAction(id, _controller);
        RollDiceAction.StartAction += StartRollDice;
        RollDiceAction.EndAction += EndRollDice;

        MoveAction.InitAction(id, _controller);
        MoveAction.StartAction += StartMoving;
        MoveAction.EndAction += EndMoving;

        LuckyDrawAction.InitAction(id, _controller);
        LuckyDrawAction.StartAction += StartLuckyDraw;
        LuckyDrawAction.EndAction += EndLuckyDraw;

        ChanceAction.InitAction(id, _controller);
        ChanceAction.StartAction += StartChance;
        ChanceAction.EndAction += EndChance;

        CombatAction.InitAction(id, _controller);
        CombatAction.StartAction += StartCombat;
        CombatAction.EndAction += EndCombat;
    }
    #endregion

    #region Turn Management
    public Action GetAction(ACTION_TYPE actionType, object[] args = null)
    {
        return actionType switch
        {
            ACTION_TYPE.MOVE => MoveAction,
            ACTION_TYPE.ROLL_DICE => RollDiceAction,
            ACTION_TYPE.END_TURN => EndTurnAction,
            ACTION_TYPE.CHANCE => ChanceAction,
            ACTION_TYPE.COMBAT => CombatAction,
            ACTION_TYPE.LUCKY_DRAW => LuckyDrawAction,
            _ => null,
        };
    }
    #endregion

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

    #region Event Action Trigger
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

    private void StartLuckyDraw()
    {
        OnStartLuckyDraw?.Invoke(id);
    }

    private void EndLuckyDraw()
    {
        OnEndLuckyDraw?.Invoke(id);
    }

    private void StartChance()
    {
        OnStartChance?.Invoke(id);
    }

    private void EndChance()
    {
        OnEndChance?.Invoke(id);
    }

    private void StartCombat()
    {
        OnStartCombat?.Invoke(id);
    }

    private void EndCombat()
    {
        OnEndCombat?.Invoke(id);
    }
    #endregion
}
