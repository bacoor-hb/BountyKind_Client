using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightProcess : MonoBehaviour
{
    [SerializeField]
    private LocalTestFightController localTestFightController;
    private Vector3 rootPos;
    public int currentAction;
    void Start()
    {
        FightActionTest.inFight1 += AnimateFight1;
        FightActionTest.inFight2 += AnimateFight2;
    }

    void Update()
    {
        
    }
    public void AnimateFight1(List<BattleData> battleDatas)
    {
        rootPos = localTestFightController.players[battleDatas[currentAction].currentUnit.id].GetComponent<Transform>().position;
        Vector3 targetPos = localTestFightController.players[battleDatas[currentAction].targetUnit.id].GetComponent<Transform>().position;
        if (currentAction < 3)
        {
            localTestFightController.players[battleDatas[currentAction].currentUnit.id].GetComponent<Transform>().position = new Vector3(targetPos.x, targetPos.y, targetPos.z - 1);
        }
        else
        {
            localTestFightController.players[battleDatas[currentAction].currentUnit.id].GetComponent<Transform>().position = new Vector3(targetPos.x, targetPos.y, targetPos.z + 1);
        }
    }

    public void AnimateFight2(List<BattleData> battleDatas)
    {
        Debug.Log("AnimateFight2");
        localTestFightController.players[battleDatas[currentAction].currentUnit.id].GetComponent<Transform>().position = rootPos;
        currentAction++;
    }
}

