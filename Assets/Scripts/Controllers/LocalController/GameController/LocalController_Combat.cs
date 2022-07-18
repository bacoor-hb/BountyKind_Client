using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public partial class LocalGameController
{
    #region Combat Control
    public void StartTurnbase_Combat(bool _skip)
    {
        Debug.Log("[LocalGameController] Start_Combat: " + _skip);
        
        LocalGameView.CombatGameView.ClosePopup();
        LocalGameView.CombatGameView.OnClosePopupFinish = null;
        LocalGameView.CombatGameView.OnClosePopupFinish += () =>
        { 
            GameEventController.Handle_Combat(_skip);
            LocalGameView.CombatGameView.OnClosePopupFinish = null;
        };
    }

    private void ReturnData_CombatStart(BattleData battle_MSG)
    {
        if (battle_MSG != null && !battle_MSG.skip)
        {
            Debug.Log("[LocalGameController] Start Combat: " + JsonConvert.SerializeObject(battle_MSG));
            fightController.OnBattleEnded = null;
            fightController.OnBattleEnded += () =>
            {
                CombatEnd();
                fightController.OnBattleEnded = null;
            };
            
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
        Debug.Log("[LocalGameController] CombatEnd...");
        SwitchCamera(0);
        LocalGameView.SetCanvasRootState(true);

        TurnBaseController.EndAction();
    }

    public void EndTurnBase_Combat()
    {
        Debug.Log("[LocalGameController] End Combat...");
        EndTurn_Action();
    }
    #endregion
}
