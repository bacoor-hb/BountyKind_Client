using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BountyKind;

[SerializeField]
public class BattleData
{
    public bool skip;
    public int status;
    public BattleProgess[] battleProgress;
    public Reward_MSG[] rewards;

    public BattleData() { }
    public BattleData(bool _skip, int _status, BattleProgess[] _battleProgress)
    {
        skip = _skip;
        status = _status;
        battleProgress = _battleProgress;
    }
}
[SerializeField]
public class BattleProgess
{
    public BattleUnit attacker;
    public BattleUnit target;
    public int turn;
    public int order;
    public string type;

    public BattleProgess() { }
    public BattleProgess(BattleUnit _attacker, BattleUnit _target, int _turn, int _order, string _type)
    {
        attacker = _attacker;
        target = _target;
        turn = _turn;
        order = _order;
        type = _type;
    }
}
[SerializeField]
public class BattleUnit
{
    public int atk;
    public int def;
    public int speed;
    public int hp;
    public string _id;
    public string faction;
    public BattleUnit(int _atk, int _def, int _speed, int _hp, string __id, string _faction)
    {
        atk = _atk;
        def = _def;
        speed = _speed;
        hp = _hp;
        _id = __id;
        faction = _faction;
    }
}
public class FightActionTest : Action
{
    public delegate void OnEndTurnFights();
    public OnEndTurnFights onEndTurnFights;
    public delegate void InFight(BattleProgess battleData, int userId);
    public static event InFight inFight;
    public delegate void OnStartFight(string unitId, int turn);
    public OnStartFight startFight;
    public delegate void EndFight(int userId);
    public EndFight endFight;
    public BattleProgess[] battleData;

    public void Init()
    {
        LocalTestFightController.OnReceiveBattleDatas += HandleReceiveBattleDatas;
        UnitController.onEndFight += HandleOnEndFight;
        UnitQueueController.OnEndQueue += OnFightAction;
    }
    public void Start()
    {

    }
    public override void InitAction(int _userId, TurnBaseController _controller)
    {
        base.InitAction(_userId, _controller);
    }

    public override void OnEndAction()
    {
        base.OnEndAction();
    }

    public int currentAction = 0;
    public override void OnStartAction()
    {
        base.OnStartAction();
        startFight(battleData[currentAction].attacker._id, battleData[currentAction].turn);
    }

    public void OnFightAction()
    {
        OnFight();
    }

    void HandleReceiveBattleDatas(BattleProgess[] _battleData, int playerId)
    {
        if (playerId == base.userId)
        {
            currentAction = 0;
            battleData = _battleData;
        }
    }

    public void OnFight()
    {
        inFight(battleData[currentAction], base.userId);
    }

    void HandleOnEndFight()
    {
        if (currentAction != battleData.Length - 1)
        {
            Debug.Log("[HandleOnEndFight]");
            currentAction++;
            turnBaseController.EndAction();
        }
        else
        {
            currentAction = 0;
            turnBaseController.EndAction();
            turnBaseController.EndTurn();
            onEndTurnFights?.Invoke();
        }
    }

    private void OnDestroy()
    {
        Debug.Log("[OnDestroyFightActionTest]");
        LocalTestFightController.OnReceiveBattleDatas -= HandleReceiveBattleDatas;
        UnitController.onEndFight -= HandleOnEndFight;
        UnitQueueController.OnEndQueue -= OnFightAction;
    }
}