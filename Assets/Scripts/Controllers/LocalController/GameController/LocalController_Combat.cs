using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LocalGameController
{
    #region Combat Control
    public void Start_Combat(bool _skip)
    {
        Debug.Log("[LocalGameController] Start_Combat: " + _skip);
        
        LocalGameView.CombatGameView.ClosePopup();
        LocalGameView.CombatGameView.OnClosePopupFinish = null;
        LocalGameView.CombatGameView.OnClosePopupFinish += () => GameEventController.Handle_Combat(_skip);
    }

    private void CombatStart(BattleData battle_MSG)
    {
        if (battle_MSG != null && !battle_MSG.skip)
        {
            Debug.Log("[LocalGameController] Start Combat...");
            fightController.OnBattleEnded = null;
            fightController.OnBattleEnded += CombatEnd;
            
            SwitchCamera(1);

            LocalGameView.SetCanvasRootState(false);
            fightController.localViewManager.SetViewState(true);
            fightController.InsertBattleData(battle_MSG);
            fightController.ProcessCharactersPosition();
        }
        else
        {
            Debug.Log("[LocalGameController] Deny Combat...");
            TurnBaseController.EndAction();
        }
    }

    private void CombatEnd()
    {
        SwitchCamera(0);
        LocalGameView.SetCanvasRootState(true);

        TurnBaseController.EndAction();
    }

    public void EndTurnCombat()
    {
        Debug.Log("[LocalGameController] End Combat...");
        EndTurn_Action();
    }
    #endregion
}
