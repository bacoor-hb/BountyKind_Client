using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRoomController : MonoBehaviour
{
    [SerializeField]
    private Button RollBTN;
    [SerializeField]
    private Button ExitBTN;
    [SerializeField]
    private InputField RollNumberInput;
    // Start is called before the first frame update
    void Start()
    {
        BountyColyseusManager.onRoomMessage += HandleOnMessage;
        RollBTN.onClick.AddListener(() => { HandleRoll(); });
        ExitBTN.onClick.AddListener(() => { HandleExitGame(); });
        RollNumberInput.text = UserManagement.Instance.GetUser().rollNumber.ToString();
    }

    void HandleRoll()
    {
        UserManagement.Instance.Roll();
    }

    void HandleExitGame() {
        UserManagement.Instance.ExitGame();
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

    // Update is called once per frame
    void Update()
    {
        RollNumberInput.text = UserManagement.Instance.GetUser().rollNumber.ToString();
    }

    public void RefillSuccess(string _rollNumber)
    {
        int rollNumber = int.Parse(_rollNumber);
        UserManagement.Instance.SetRollNumber(rollNumber);
        Debug.Log("Refilled");
    }
}
