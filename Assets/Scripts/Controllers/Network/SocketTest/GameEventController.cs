using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameEventController : MonoBehaviour
{
    [SerializeField]
    private InputField RollNumberInput;

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
        RollNumberInput.text = UserDataManager.UserData.rollNumber.ToString();
    }

    void HandleOnMessage(string messageType, object message)
    {
        switch (messageType)
        {
            case PLAYER_RECEIVE_EVENTS.ROLL_RESULT:
                RollResultMessage rollMessage = (RollResultMessage) message;
                Debug.Log(rollMessage.totalStep);
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
        if (UserDataManager.UserData.rollNumber > 0)
        {
            NetworkManager.Send(PLAYER_SENT_EVENTS.ROLL_DICE);
            //UserDataManager.UserData.rollNumber--;
        }
        else
        {
            //Call external Refill feature.
            NetworkManager.Refill_External();
        }
    }

    void HandleExitGame()
    {
        NetworkManager.ExitGame();
    }
    #endregion

    public void RefillSuccess(string _rollNumber)
    {
        int rollNumber = int.Parse(_rollNumber);
        UserDataManager.SetRollNumber(rollNumber);
        Debug.Log("Refilled");
    }
}
