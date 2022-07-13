using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BountyKind;

public class UserActionLocalManager : IPlayer
{
    public delegate void OnEventTriggered<T>(T data);
    public OnEventTriggered<bool> OnStartRollingDice;
    public OnEventTriggered<int> OnEndRollingDice;
    public OnEventTriggered<int> OnStartMoving;
    public OnEventTriggered<int> OnEndMoving;
    public OnEventTriggered<bool> OnStartLuckyDraw;
    public OnEventTriggered<int> OnEndLuckyDraw;
    public OnEventTriggered<bool> OnStartChance;
    public OnEventTriggered<int> OnEndChance;
    public OnEventTriggered<bool> OnStartCombat;
    public OnEventTriggered<int> OnEndCombat;

    [Header("Turn Action")]
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

    [Header("Users Anim Action Control")]
    [SerializeField]
    private List<UserAnimationController> UserAnim;

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

        for (int i = 0; i < UserAnim.Count; i++)
        {
            UserAnim[i].Walking(CONSTS.ANIM_SPEED_IDLE);
        }
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
        Debug.Log("StartTurn: id: " + id);
    }

    public override void EndTurn()
    {
        Debug.Log("EndTurn: id: " + id);
    }

    #region Event Action Trigger
    public bool skipRollDice { get; private set; }
    public void SetSkipRollDice(bool _Skip)
    {
        skipRollDice = _Skip;
    }

    private void StartRollDice()
    {
        Debug.Log("[UserActionLocalManager] StartRollDice.");
        OnStartRollingDice?.Invoke(skipRollDice);
    }

    private void EndRollDice()
    {
        Debug.Log("[UserActionLocalManager] EndRollDice.");
        OnEndRollingDice?.Invoke(id);
    }

    private void StartMoving()
    {
        OnStartMoving?.Invoke(id);
        UserAnim[id].Walking(CONSTS.ANIM_SPEED_RUN);
    }

    private void EndMoving()
    {
        OnEndMoving?.Invoke(id);
        UserAnim[id].Walking(CONSTS.ANIM_SPEED_IDLE);
    }

    public bool skipLuckyDraw { get; private set; }
    public void SetSkipLuckyDraw(bool _Skip)
    {
        skipRollDice = _Skip;
    }
    private void StartLuckyDraw()
    {
        OnStartLuckyDraw?.Invoke(skipLuckyDraw);
    }

    private void EndLuckyDraw()
    {
        OnEndLuckyDraw?.Invoke(id);
    }

    public bool skipChance { get; private set; }
    public void SetSkipChance(bool _Skip)
    {
        skipChance = _Skip;
    }
    private void StartChance()
    {
        OnStartChance?.Invoke(skipChance);
    }

    private void EndChance()
    {
        OnEndChance?.Invoke(id);
    }

    public bool skipCombat {get; private set; }
    public void SetSkipCombat(bool _Skip)
    {
        skipCombat = _Skip;
    }
    private void StartCombat()
    {        
        OnStartCombat?.Invoke(skipCombat);
    }
    private void EndCombat()
    {
        OnEndCombat?.Invoke(id);
    }
    #endregion
}
