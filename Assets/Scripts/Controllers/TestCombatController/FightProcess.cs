using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightProcess : MonoBehaviour
{
    public delegate void OnAnimTrigger<T>(T data);
    public OnAnimTrigger<int> OnStartFightTrigger;
    public OnAnimTrigger<int> OnEndFightTrigger;
    [SerializeField]
    private LocalTestFightController localTestFightController;
    void Start()
    {
        FightActionTest.inFight += AnimateFight;
    }

    void Update()
    {

    }
    public void AnimateFight(BattleData battleData, int userId)
    {
        GameObject currentGameObj;
        GameObject targetGameObj;
        currentGameObj = localTestFightController.players[userId].characters[battleData.currentUnit.id];
        targetGameObj = localTestFightController.players[userId].characters[battleData.targetUnit.id];
        UnitController prefabController = currentGameObj.GetComponent<UnitController>();
        Vector3 targetPos = targetGameObj.GetComponent<Transform>().position;
        prefabController.HandleMove(targetPos, "Crouch_b");

    }
}

