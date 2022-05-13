using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTestFightController : IPlayer
{
    [SerializeField]
    private FightActionTest FightAction;
    [SerializeField]
    private Action EndTurnAction;

    private TurnBaseController TurnBaseController;

    /// <summary>
    /// Initialize the Player and all of his Actions
    /// </summary>
    /// <param name="_id"></param>
    public void InitPlayer(int _id, TurnBaseController _controller)
    {
        id = _id;
        InitPlayerAction(_controller);
    }

    /// <summary>
    /// Initialize the player Actions
    /// </summary>
    private void InitPlayerAction(TurnBaseController _controller)
    {
        TurnBaseController = _controller;

        FightAction.InitAction(id, _controller);
        FightAction.StartAction += OnFightStart;
        FightAction.EndAction += OnFightEnd;

        EndTurnAction.InitAction(id, _controller);
    }

    #region Turn Management
    public Action GetAction(ACTION_TYPE actionType)
    {
        return actionType switch
        {
            ACTION_TYPE.FIGHT => FightAction,
            ACTION_TYPE.END_TURN => EndTurnAction,
            _ => null,
        };
    }

    public override void StartTurn()
    {
        Material myMaterial = GetComponent<Renderer>().material;
        myMaterial.color = Color.red;
        Debug.Log("StartTurn: id: " + id);
    }

    public override void EndTurn()
    {
        Material myMaterial = GetComponent<Renderer>().material;
        myMaterial.color = Color.white;
        Debug.Log("EndTurn: id: " + id);
    }
    #endregion      
    public void OnFightStart()
    {
        Debug.Log("[OnFightStart] OnFightStart on PLayer: OnFightStart | id: " + id);
    }
    public void OnFightEnd()
    {
        Debug.Log("[OnFightEnd] OnFightEnd on PLayer | id: " + id);
    }
}
