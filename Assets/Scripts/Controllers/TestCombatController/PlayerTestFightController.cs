using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTestFightController : IPlayer
{
    [SerializeField]
    private FightActionTest FightAction;
    [SerializeField]
    private Action EndTurnAction;
    [SerializeField]
    public Dictionary<string, GameObject> characters = new Dictionary<string, GameObject>();

    private TurnBaseController TurnBaseController;

    /// <summary>
    /// Initialize the Player and all of his 
    /// </summary>
    /// <param name="_id"></param>
    /// 
    public void AddUnit(GameObject unit, string unitId)
    {
        characters.Add(unitId, unit);
    }

    public GameObject RenderUnit(GameObject prefab, Vector3 pos, Transform parent)
    {
        return Instantiate(prefab, pos, Quaternion.identity, parent);
    }
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
            ACTION_TYPE.COMBAT => FightAction,
            ACTION_TYPE.END_TURN => EndTurnAction,
            _ => null,
        };
    }

    public override void StartTurn()
    {


    }

    public override void EndTurn()
    {
        Debug.Log("EndTurn: id: " + id);
    }
    #endregion      
    public void OnFightStart()
    {

    }
    public void OnFightEnd()
    {
        Debug.Log("[OnFightEnd] OnFightEnd on PLayer | id: " + id);
    }
}
