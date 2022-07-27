using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;

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
            if(!_skip)
            {
                Open_Formation();                
            }
            else
            {
                GameEventController.Handle_Combat(_skip);
                TurnBaseController.EndAction();
            }
            LocalGameView.CombatGameView.OnClosePopupFinish = null;
        };

        if(!_skip)
        {
            LocalFightController.InitPlayers();
            LocalFightController.StartGame();
        }        
    }

    private void Open_Formation()
    {
        SwitchCamera(CAMERA_PARENT.FORMATION);
        LocalGameView.SetCanvasRootState(false);
        FormationViewManager.SetFormationCanvasState(true);
        FormationController.Instance.GetUserFormationData();
        FormationController.Instance.OnSetFormationSuccess += () =>
        {
            bool _skip = currentPlayer.skipCombat;
            GameEventController.Handle_Combat(_skip);
            FormationController.Instance.OnSetFormationSuccess = null;
        };
    }

    private void ReturnData_CombatStart(BattleData battle_MSG)
    {
        if (battle_MSG != null && !battle_MSG.skip)
        {
            Debug.Log("[LocalGameController] Start Combat: " + JsonConvert.SerializeObject(battle_MSG));
            StartCombatProcess(battle_MSG);
            //TurnBaseController.EndAction();
        }
        else
        {
            Debug.Log("[LocalGameController] Deny Combat...");
            TurnBaseController.EndAction();
        }
    }

    private void StartCombatProcess(BattleData battle_MSG)
    {
        LocalFightController.OnBattleEnded = null;
        LocalFightController.OnBattleEnded += () =>
        {
            EndCombat_Process();
            LocalFightController.OnBattleEnded = null;
        };

        SwitchCamera(CAMERA_PARENT.COMBAT);
        FormationViewManager.SetFormationCanvasState(false);

        LocalFightController.localViewManager.SetViewState(true);
        LocalFightController.InsertBattleData(battle_MSG);
        LocalFightController.SetUserInfomation();
    }

    private void EndCombat_Process()
    {
        Debug.Log("[LocalGameController] CombatEnd...");
        SwitchCamera(CAMERA_PARENT.BOARD);
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
