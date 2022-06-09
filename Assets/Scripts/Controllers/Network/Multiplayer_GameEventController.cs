using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplayer_GameEventController : MonoBehaviour
{
    public delegate void OnEventReturn<T>(T returnData);
    public OnEventReturn<RollResult_MSG> OnRollResultReturn;

    private UserDataManager UserDataManager;
    private NetworkManager NetworkManager;

    // Start is called before the first frame update
    public void Init()
    {
        UserDataManager = GlobalManager.Instance.UserDataManager;
        NetworkManager = GlobalManager.Instance.NetworkManager;

        
        BountyColyseusManager.Instance.onGameReceiveMsg = null;
        BountyColyseusManager.Instance.onGameReceiveMsg += HandleGameMessage;
    }



    #region Handle Game Event
    void HandleGameMessage(GAMEROOM_RECEIVE_EVENTS messageType, object message)
    {
        try
        {
            switch (messageType)
            {
                case GAMEROOM_RECEIVE_EVENTS.ROLL_RESULT:
                    RollResult_MSG rollMessage = JsonUtility.FromJson<RollResult_MSG>(message.ToString());
                    Debug.Log("[GameEventController] Roll Success...");
                    OnRollSuccess(rollMessage);
                    break;
                case GAMEROOM_RECEIVE_EVENTS.FIGHT_RESULT:
                    Debug.Log("FIGHT_RESULT");
                    break;
                case GAMEROOM_RECEIVE_EVENTS.LUCKY_DRAW_RESULT:
                    Debug.Log("LUCKY_DRAW_RESULT");
                    break;
                case GAMEROOM_RECEIVE_EVENTS.BALANCE_RESULT:
                    Debug.Log("BALANCE_RESULT");
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("[HandleGameMessage] ERROR:" + ex.Message);
        }        
    }


    /// <summary>
    /// Handle the Roll Dice Event with the Server.
    /// </summary>
    public void HandleRoll()
    {
        if (UserDataManager.GetEnergy() > 0)
        {
            Debug.Log("[Multiplayer_GameEventController] Send Roll event...");
            NetworkManager.Send(SEND_TYPE.GAMEROOM_SEND, GAMEROOM_SENT_EVENTS.ROLL_DICE.ToString());
        }
        else
        {
            //Call external Refill feature.
            NetworkManager.Refill_External();
        }
    }

    void OnRollSuccess(RollResult_MSG rollResult)
    {
        Debug.Log("[Multiplayer_GameEventController] OnRollSuccess");
        UserDataManager.UserGameStatus.UpdateGameStatus(rollResult);
        OnRollResultReturn?.Invoke(rollResult);
    }

    public void HandleLuckyDraw()
    {
        Debug.Log("[Multiplayer_GameEventController] Send Lucky Draw...");
        NetworkManager.Send(SEND_TYPE.GAMEROOM_SEND, GAMEROOM_SENT_EVENTS.LUCKY_DRAW.ToString());
    }

    void HandleExitGame()
    {
        NetworkManager.Disconnect();
    }
    #endregion

    public void RefillSuccess(string _rollNumber)
    {
        int rollNumber = int.Parse(_rollNumber);
        UserDataManager.SetEnergy(rollNumber);
        Debug.Log("Refilled");
    }
}
