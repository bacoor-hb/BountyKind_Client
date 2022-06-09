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

    private UserDataManager userData;

    [Header ("Turn Action")]
    [SerializeField]
    private Action EndTurnAction;
    [SerializeField]
    private Action RollDiceAction;
    [SerializeField]
    private Action MoveAction;

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
        userData = GlobalManager.Instance.UserDataManager;

        TurnBaseController = _controller;

        EndTurnAction.InitAction(id, _controller);

        RollDiceAction.InitAction(id, _controller);
        RollDiceAction.StartAction += StartRollDice;
        RollDiceAction.EndAction += EndRollDice;

        MoveAction.InitAction(id, _controller);
        MoveAction.StartAction += StartMoving;
        MoveAction.EndAction += EndMoving;
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
