using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightActionTest : Action
{
    public delegate void InFight(List<BattleData> battleDatas);
    public static event InFight inFight1;
    public static event InFight inFight2;
    public List<BattleData> battleDatas;

    public override void InitAction(int _userId, TurnBaseController _controller)
    {
        InsertFakeData();
        base.InitAction(_userId, _controller);
    }

    public override void ClearEvent()
    {
        base.ClearEvent();
    }

    public override void OnEndAction()
    {
        base.OnEndAction();
    }

    public override void OnStartAction()
    {
        base.OnStartAction();
        OnFightAction();
    }

    int auctionActionFlag = 0;
    public void OnFightAction()
    {
        auctionActionFlag = 0;
        StartCoroutine(OnFight());
    }

    void InsertFakeData()
    {
        battleDatas = new List<BattleData>();
        BattleUnit currentUnit1 = new BattleUnit(0, 100);
        BattleUnit targetUnit1 = new BattleUnit(3, 90);
        BattleUnit currentUnit2 = new BattleUnit(1, 100);
        BattleUnit targetUnit2 = new BattleUnit(3, 80);
        BattleUnit currentUnit3 = new BattleUnit(2, 100);
        BattleUnit targetUnit3 = new BattleUnit(3, 70);

        battleDatas.Add(new BattleData(currentUnit1, targetUnit1));
        battleDatas.Add(new BattleData(currentUnit2, targetUnit2));
        battleDatas.Add(new BattleData(currentUnit3, targetUnit3));

        BattleUnit currentUnit4 = new BattleUnit(3, 70);
        BattleUnit targetUnit4 = new BattleUnit(0, 90);
        BattleUnit currentUnit5 = new BattleUnit(4, 100);
        BattleUnit targetUnit5 = new BattleUnit(0, 80);
        BattleUnit currentUnit6 = new BattleUnit(5, 100);
        BattleUnit targetUnit6 = new BattleUnit(0, 70);

        battleDatas.Add(new BattleData(currentUnit4, targetUnit4));
        battleDatas.Add(new BattleData(currentUnit5, targetUnit5));
        battleDatas.Add(new BattleData(currentUnit6, targetUnit6));
    }

    public IEnumerator OnFight()
    {
        inFight1(battleDatas);
        yield return new WaitForSeconds(1);
        inFight2(battleDatas);
        yield return new WaitForSeconds(1);
        turnBaseController.EndAction();
    }
}