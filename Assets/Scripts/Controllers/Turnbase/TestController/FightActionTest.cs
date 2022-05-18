using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleUnit
{
    public int id;
    public int health;
    public BattleUnit(int _id, int _health)
    {
        id = _id;
        health = _health;
    }
}
public class BattleData
{
    public BattleUnit currentUnit;
    public BattleUnit targetUnit;
    public string type;
    public BattleData(BattleUnit _currentUnit, BattleUnit _targetUnit, string _type)
    {
        currentUnit = _currentUnit;
        targetUnit = _targetUnit;
        type = _type;
    }
}
public class FightActionTest : Action
{
    public delegate void OnEndTurnFights();
    public static event OnEndTurnFights onEndTurnFights;
    public delegate void InFight(BattleData battleData, int userId);
    public static event InFight inFight;
    public List<BattleData> battleDatas;

    public void Start()
    {
        LocalTestFightController.onReceiveBattleDatas += HandleReceiveBattleDatas;
        UnitController.onEndFight += HandleOnEndFight;
    }
    public override void InitAction(int _userId, TurnBaseController _controller)
    {
        base.InitAction(_userId, _controller);
    }

    public override void OnEndAction()
    {
        base.OnEndAction();
    }

    int currentAction = 0;
    public override void OnStartAction()
    {
        base.OnStartAction();
        OnFightAction();
    }

    public void OnFightAction()
    {
        OnFight();
    }

    void HandleReceiveBattleDatas(List<BattleData> _battleDatas, int playerId)
    {
        if (playerId == base.userId)
        {
            currentAction = 0;
            battleDatas = _battleDatas;
        }
    }

    public void OnFight()
    {
        inFight(battleDatas[currentAction], base.userId);
    }

    void HandleOnEndFight()
    {
        if (currentAction != 5)
        {
            currentAction++;
            turnBaseController.EndAction();
        }
        else
        {
            currentAction = 0;
            turnBaseController.EndAction();
            turnBaseController.EndTurn();
        }

    }
}