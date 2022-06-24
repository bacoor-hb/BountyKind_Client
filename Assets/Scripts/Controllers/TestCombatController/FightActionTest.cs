using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BountyKind;

public class BattleUnit
{
    public string id;
    public int health;
    public BattleUnit(string _id, int _health)
    {
        id = _id;
        health = _health;
    }
}
public class BattleData
{
    public BattleUnit currentUnit;
    public BattleUnit targetUnit;
    public int turn;
    public BattleData(BattleUnit _currentUnit, BattleUnit _targetUnit, int _turn)
    {
        currentUnit = _currentUnit;
        targetUnit = _targetUnit;
        turn = _turn;
    }
}
public class FightActionTest : Action
{
    public delegate void OnEndTurnFights();
    public static event OnEndTurnFights onEndTurnFights;
    public delegate void InFight(BattleData battleData, int userId);
    public static event InFight inFight;
    public delegate void OnStartFight(string unitId, int turn);
    public static event OnStartFight startFight;
    public delegate void EndFight(int userId);
    public static event EndFight endFight;
    public List<BattleData> battleDatas;

    public void Start()
    {
        LocalTestFightController.onReceiveBattleDatas += HandleReceiveBattleDatas;
        UnitController.onEndFight += HandleOnEndFight;
        UnitQueueController.onEndQueue += OnFightAction;

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
        startFight(battleDatas[currentAction].currentUnit.id, battleDatas[currentAction].turn);
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
        if (currentAction != battleDatas.Count - 1)
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