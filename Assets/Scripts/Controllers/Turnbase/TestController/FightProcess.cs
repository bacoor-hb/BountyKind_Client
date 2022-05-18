using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightProcess : MonoBehaviour
{
    public delegate void OnEndFightAnimation();

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
        if (battleData.type == "YOUR_PET")
        {
            currentGameObj = localTestFightController.players[userId].yourPets[battleData.currentUnit.id];
            targetGameObj = localTestFightController.players[userId].opponentPets[battleData.targetUnit.id];
        }
        else
        {
            currentGameObj = localTestFightController.players[userId].opponentPets[battleData.currentUnit.id];
            targetGameObj = localTestFightController.players[userId].yourPets[battleData.targetUnit.id];
        }

        UnitController prefabController = currentGameObj.GetComponent<UnitController>();
        Vector3 targetPos = targetGameObj.GetComponent<Transform>().position;
        prefabController.HandleMove(targetPos, "Crouch_b");

    }
}

