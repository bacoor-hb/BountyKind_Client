using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LocalGameController
{
    #region Lucky Draw
    public void Start_LuckyDraw()
    {
        Debug.Log("[LocalGameController] Start LuckyDraw...");

        //Initialize the Luckydraw Process----------------------------------------------------------------
        bool _skip = currentPlayer.skipLuckyDraw;
        LuckyDrawView luckyDrawView = LocalGameView.LuckyDrawPopup;
        LocalGameView.LuckyDrawPopup.Init_Phase2(_skip);

        if (_skip)
        {
            //Skip Lucky draw
            luckyDrawView.ClosePopup(LUCKYDRAW_POPUP.INVITATION);
            luckyDrawView.OnLuckyDraw_CloseInvPopupEnd = null;
            luckyDrawView.OnLuckyDraw_CloseInvPopupEnd += (skip) =>
            {
                GameEventController.HandleLuckyDraw(skip);
                TurnBaseController.EndAction();
                luckyDrawView.OnLuckyDraw_CloseInvPopupEnd = null;
            };
        }
        else
        {
            luckyDrawView.ClosePopup(LUCKYDRAW_POPUP.INVITATION);
            luckyDrawView.OnLuckyDraw_CloseInvPopupEnd = null;
            luckyDrawView.OnLuckyDraw_CloseInvPopupEnd += (x) =>
            {
                luckyDrawView.OpenPopup(LUCKYDRAW_POPUP.ROLL);
                luckyDrawView.OnLuckyDraw_CloseInvPopupEnd = null;
            };
            
            //Handle Lucky draw while clicking the Roll Button
            luckyDrawView.OnLuckyDraw_StartDraw = null;
            luckyDrawView.OnLuckyDraw_StartDraw += (skip) =>
            {
                GameEventController.HandleLuckyDraw(false);
                luckyDrawView.OnLuckyDraw_StartDraw = null;
            };

            Multiplayer_GameEvent.OnLuckyDrawReturn = null;
            Multiplayer_GameEvent.OnLuckyDrawReturn += (msg) =>
            {
                string winningItemID = msg.key;
                int winningGameID = 0;
                luckyDrawView.LuckyDraw_EndDraw(winningGameID);

                luckyDrawView.OnLuckyDraw_EndDraw = null;
                luckyDrawView.OnLuckyDraw_EndDraw += () =>
                {
                    LocalGameView.LuckyDrawPopup.Init_Phase3();
                    luckyDrawView.ClosePopup(LUCKYDRAW_POPUP.ROLL);
                    luckyDrawView.OnLuckyDraw_EndDraw = null;
                };                
                
                luckyDrawView.OnLuckyDraw_CloseRollPopupEnd = null;
                luckyDrawView.OnLuckyDraw_CloseRollPopupEnd += (skip) =>
                {                    
                    luckyDrawView.OpenPopup(LUCKYDRAW_POPUP.CONGRAT);
                    luckyDrawView.OnLuckyDraw_CloseRollPopupEnd = null;
                };
                Multiplayer_GameEvent.OnLuckyDrawReturn = null;
            };

            luckyDrawView.OnLuckyDraw_CloseCongratPopupEnd = null;
            luckyDrawView.OnLuckyDraw_CloseCongratPopupEnd += (skip) =>
            {
                TurnBaseController.EndAction();
                luckyDrawView.OnLuckyDraw_CloseCongratPopupEnd = null;
            };
        }        
    }

    public void End_LuckyDraw()
    {
        Debug.Log("[LocalGameController] End LuckyDraw...");
        EndTurn_Action();
    }

    public void GetReward_LuckyDraw(Reward_MSG reward)
    {
        Debug.Log("LocalGameController" + JsonUtility.ToJson(reward));
    }
    #endregion
}
