using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplayer_GameEventController : MonoBehaviour
{
    [SerializeField]
    private LocalGameView LocalGameView;
    [SerializeField]
    private LocalGameController LocalGameController;

    private UserDataManager UserDataManager;
    private MapNodeDataManager MapNodeDataManager;
    private NetworkManager NetworkManager;

    // Start is called before the first frame update
    public void Init()
    {
        UserDataManager = GlobalManager.Instance.UserDataManager;
        MapNodeDataManager = GlobalManager.Instance.MapNodeDataManager;
        NetworkManager = GlobalManager.Instance.NetworkManager;

        
        BountyColyseusManager.Instance.onGameReceiveMsg = null;
        BountyColyseusManager.Instance.onGameReceiveMsg += HandleGameMessage;

        LocalGameView.RollDice_Btn.onClick.AddListener(() => { HandleRoll(); });
        LocalGameView.Exit_Btn.onClick.AddListener(() => { HandleExitGame(); });
    }



    #region Handle Game Event
    void HandleGameMessage(GAMEROOM_RECEIVE_EVENTS messageType, object message)
    {
        try
        {
            switch (messageType)
            {
                case GAMEROOM_RECEIVE_EVENTS.ROLL_RESULT:
                    RollResultSchema rollMessage = (RollResultSchema)message;
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



    void HandleRoll()
    {
        if (UserDataManager.GetEnergy() > 0)
        {
            NetworkManager.Send(SEND_TYPE.GAMEROOM_SEND, GAMEROOM_SENT_EVENTS.ROLL_DICE.ToString());
        }
        else
        {
            //Call external Refill feature.
            NetworkManager.Refill_External();
        }
    }

    void OnRollSuccess(RollResultSchema rollResult)
    {
        LocalGameView.UpdateUserData(UserDataManager.UserData);
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
