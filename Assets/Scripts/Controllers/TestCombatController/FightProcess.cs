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
    public void AnimateFight(BattleProgess battleData, int userId)
    {
        GameObject currentGameObj;
        GameObject targetGameObj;
        PlayerTestFightController playerController = localTestFightController.players[userId].GetComponent<PlayerTestFightController>();
        currentGameObj = playerController.characters[battleData.attacker._id];
        targetGameObj = playerController.characters[battleData.target._id];
        UnitController prefabController = currentGameObj.GetComponent<UnitController>();
        Vector3 targetPos = targetGameObj.transform.position;
        prefabController.HandleMove(targetPos, battleData);

    }
}

