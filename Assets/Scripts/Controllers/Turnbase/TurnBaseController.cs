using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBaseController : MonoBehaviour
{
    public delegate void Event<T>(T data);
    public Event<int> OnStartGame;
    public Event<int> OnEndGame;
    public Event<int> OnStartTurn;
    public Event<int> OnEndTurn;
    public Event<int> OnChangePlayer;

    public bool IsStarting { get; private set; }
    public int CurrentPlayer { get; private set; }
    [HideInInspector]
    public List<IPlayer> playerList;

    private Action currentAction;
    private Queue<Action> queueActionList;

    private CYCLE_TURN status;
    private bool isWaiting;

    #region Unity Event
    public void Init()
    {
        IsStarting = false;
        playerList = new List<IPlayer>();
        status = CYCLE_TURN.START_TURN;
        isWaiting = false;
    }

    private void Update()
    {
        // check game start, and status waiting
        if (IsStarting && !isWaiting)
        {
            switch (status)
            {
                case CYCLE_TURN.START_TURN:
                    StartTurn();
                    break;
                case CYCLE_TURN.WAITING_ACTION:
                    CheckActionInQueue();
                    break;
                case CYCLE_TURN.START_ACTION:
                    OnAction();
                    break;
                case CYCLE_TURN.END_ACTION:
                    NextStep();
                    break;
                case CYCLE_TURN.END_TURN:
                    EndTurn();
                    break;
            }
        }
    }
    #endregion

    #region Action
    /// <summary>
    /// add action when your turn
    /// </summary>
    public void AddAction(IPlayer player, Action action)
    {
        if (IsStarting)
        {
            if (queueActionList != null && player == playerList[CurrentPlayer])
            {
                queueActionList.Enqueue(action);
            }
        }
        else
        {
            Debug.LogError("[AddAction] ERROR: Game is not start.");
        }
    }

    /// <summary>
    /// handle law in game
    /// </summary>
    private void OnAction()
    {
        isWaiting = true;        
    }

    /// <summary>
    ///  Execute the Action in the Queue.
    /// </summary>
    private void CheckActionInQueue()
    {
        if (queueActionList.Count > 0)
        {
            currentAction = queueActionList.Dequeue();

            Debug.Log("[TurnbaseController][CheckActionInQueue] Start Action: " + currentAction.GetAction().ToString());
            
            currentAction.OnStartAction();

            NextStep();
        }
    }

    /// <summary>
    ///  Script run after excute action
    /// </summary>
    public void EndAction()
    {
        Debug.Log("[TurnbaseController][EndAction] Current State: " + status.ToString());
        currentAction.OnEndAction();
        NextStep();
    }
    #endregion

    #region Turn Management
    /// <summary>
    ///  Step cycle turn
    /// </summary>
    private void NextStep()
    {
        isWaiting = false;
        Debug.Log("[NextStep] current step: " + status.ToString());
        switch (status)
        {
            case CYCLE_TURN.START_TURN:
                status = CYCLE_TURN.WAITING_ACTION;
                break;
            case CYCLE_TURN.WAITING_ACTION:
                status = CYCLE_TURN.START_ACTION;
                break;
            case CYCLE_TURN.START_ACTION:
                status = CYCLE_TURN.END_ACTION;
                break;
            case CYCLE_TURN.END_ACTION:
                if (currentAction.GetAction() == ACTION_TYPE.END_TURN)
                {
                    status = CYCLE_TURN.END_TURN;
                }
                else
                {
                    status = CYCLE_TURN.WAITING_ACTION;
                }
                break;
            case CYCLE_TURN.END_TURN:
                status = CYCLE_TURN.START_TURN;
                break;
        }
        Debug.Log("[NextStep] next step: " + status.ToString());
    }

    /// <summary>
    /// call StartGame to start game
    /// </summary>
    public void StartGame()
    {
        if (!IsStarting)
        {
            IsStarting = true;
            CurrentPlayer = 0;
            OnStartGame?.Invoke(CurrentPlayer);

            status = CYCLE_TURN.START_TURN;
            isWaiting = false;
        }
    }

    /// <summary>
    /// call EndGame to stop game
    /// </summary>
    public void EndGame()
    {
        if (IsStarting)
        {
            IsStarting = false;

            OnEndGame?.Invoke(CurrentPlayer);
        }
    }

    /// <summary>
    ///  Script run before start turn
    /// </summary>
    private void StartTurn()
    {
        // init history action in one turn
        queueActionList = new Queue<Action>();

        //Trigger the Start turn function of the current player
        playerList[CurrentPlayer].StartTurn();
        OnStartTurn?.Invoke(CurrentPlayer);

        NextStep();
    }

    /// <summary>
    ///  Script run after end turn
    /// </summary>
    private void EndTurn()
    {
        //Trigger the End turn function of the current player
        playerList[CurrentPlayer].EndTurn();
        OnEndTurn?.Invoke(CurrentPlayer);

        // handle change player
        if (CheckChangePlayer())
        {
            ChangePlayer();
        }
        NextStep();
    }

    /// <summary>
    ///  Modify the currentplayer Id. This function will also trigger the OnChangePlayer event, then reset it.
    /// </summary>
    private void ChangePlayer()
    {
        CurrentPlayer = GetNextPlayerId();

        //Invoke 1 time OnChangePlayer event and then reset it.
        OnChangePlayer?.Invoke(CurrentPlayer);
        OnChangePlayer = null;
    }

    /// <summary>
    /// Get the Id of next player in the list
    /// </summary>
    /// <returns></returns>
    private int GetNextPlayerId()
    {
        return (CurrentPlayer + 1) % playerList.Count;
    }

    /// <summary>
    /// check change player when return true
    /// </summary>
    /// <returns></returns>
    private bool CheckChangePlayer()
    {
        return true;
    }

    /// <summary>
    /// regist game
    /// </summary>
    public void Register(IPlayer player)
    {
        if (IsStarting)
        {
            Debug.LogError("[Register]: ERROR: Game is start.");
        }
        else
        {
            playerList.Add(player);
        }
    }
    #endregion

}