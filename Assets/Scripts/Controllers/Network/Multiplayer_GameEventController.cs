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
    private NetworkManager NetworkManager;

    // Start is called before the first frame update
    public void Init()
    {
        UserDataManager = GlobalManager.Instance.UserDataManager;
        NetworkManager = GlobalManager.Instance.NetworkManager;

        BountyColyseusManager.onReceiveMessage += HandleOnMessage;
        LocalGameView.RollDice_Btn.onClick.AddListener(() => { HandleRoll(); });
        LocalGameView.Exit_Btn.onClick.AddListener(() => { HandleExitGame(); });
    }

    void HandleOnMessage(string messageType, object message)
    {
        switch (messageType)
        {
            case PLAYER_RECEIVE_EVENTS.ROLL_RESULT:
                RollResultMessage rollMessage = (RollResultMessage) message;
                Debug.Log("[GameEventController] Roll Success...");
                OnRollSuccess(rollMessage);
                break;
            case PLAYER_RECEIVE_EVENTS.FIGHT_RESULT:
                Debug.Log("FIGHT_RESULT");
                break;
            case PLAYER_RECEIVE_EVENTS.BATTLE_INIT:
                Debug.Log("BATTLE_INIT");
                break;
            case PLAYER_RECEIVE_EVENTS.ERROR:
                Debug.Log("ERROR");
                break;
            default:
                break;
        }
    }

    #region Handle Game Event
    void HandleRoll()
    {
        if (UserDataManager.GetEnergy() > 0)
        {
            NetworkManager.Send(PLAYER_SENT_EVENTS.ROLL_DICE);
        }
        else
        {
            //Call external Refill feature.
            NetworkManager.Refill_External();
        }
    }

    void OnRollSuccess(RollResultMessage rollResult)
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
