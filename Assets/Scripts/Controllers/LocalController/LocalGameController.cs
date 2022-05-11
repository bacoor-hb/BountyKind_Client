using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameController : MonoBehaviour
{
    [SerializeField]
    private List<UserManager> players;

    [Header ("Controller Modules")]
    [SerializeField]
    private MovementController MovementController;
    [SerializeField]
    private DicesController DicesController;
    [SerializeField]
    private TurnBaseController TurnBaseController;

    [Header("Game Board")]
    [SerializeField]
    private GraphNode[] nodeList;
    [SerializeField]
    private Graph board;

    [Header("Game View")]
    [SerializeField]
    private LocalGameView LocalGameView;

    //-------------------------------------------------------------------------------------------
    #region Initialize
    // Start is called before the first frame update
    void Start()
    {
        //Init Turnbase Controller
        TurnBaseController.Init();

        //Initialize all the player and set their event Actions.
        InitPlayers();

        TurnBaseController.StartGame();
        TurnBaseController.OnStartTurn = null;
        TurnBaseController.OnStartTurn += OnStartTurn;

        //Init Dice Controller
        DicesController.Init();

        //Init the Board
        board.Init();
        board.GenerateBoard(nodeList);
        GraphNode startNode = board.GetNode(0);//Set the current Node is the node 0

        //Init Move Controller
        
    }
    #endregion

    #region Get Data
    /// <summary>
    /// Get the User Data from the Server/Blockchain
    /// </summary>
    /// <returns></returns>
    User GetUser(int userID)
    {
        //Test Data, must be replace by data from Server
        User tmp = new User()
        {
            Address = "Default " + userID,
            AvatarID = 1,
            Name = "Default" + userID
        };
        return tmp;
    }
    #endregion

    #region Turn Base Controller
    int rollNumber = 0;
    UserManager currentPlayer;
    int currentPlayerId;
    void OnStartTurn(int userID)
    {
        //Get Current Player
        currentPlayerId = TurnBaseController.CurrentPlayer;
        currentPlayer = players[currentPlayerId];

        //User has 1 roll Number and 1 move number at each turn
        rollNumber = 1;

        //Init Roll Dice Event

        //Init the View + Button Event
        LocalGameView.Init();
        LocalGameView.SetUserData(currentPlayer.GetWalletData());

        LocalGameView.RollDice_Btn.onClick.AddListener(RollDice_Action);
        LocalGameView.Move_Btn.onClick.AddListener(Move_Action);
        LocalGameView.EndTurn_Btn.onClick.AddListener(EndTurn_Action);
    }

    /// <summary>
    /// Add Roll Dice Action to Queue.
    /// </summary>
    void RollDice_Action()
    {
        if (rollNumber > 0)
        {
            rollNumber--;
            TurnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.ROLL_DICE));
        }
    }
    /// <summary>
    /// Add Action Move to the Queue
    /// </summary>
    private void Move_Action()
    {
        TurnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.MOVE));
    }
    /// <summary>
    /// Add End Turn Action to the Queue
    /// </summary>
    void EndTurn_Action()
    {
        Debug.Log("[LocalGameController] End Turn...");
        TurnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.END_TURN));
    }
    #endregion

    #region Dice Controller
    int[] DicesValue;
    /// <summary>
    /// Only for TEST. The real data will be got from Server. 
    /// </summary>
    /// <returns></returns>
    public int[] GetDiceValue(int _diceNB = 1)
    {
        if(_diceNB > 0)
        {
            DicesValue = new int[_diceNB];
            for(int i = 0; i< _diceNB; i++)
            {
                DicesValue[i] = Random.Range(0, 6);
            }
            return DicesValue;
        }
        else
        {
            return null;
        }        
    }

    public void RollDice()
    {
        DicesController.RollDice(GetDiceValue());

        //Deactivate the Roll button if the roll number is 0
        if (rollNumber <= 0)
        {
            LocalGameView.SetBtn_State(ACTION_TYPE.ROLL_DICE, false);
        }        
    }

    public void End_RollDice()
    {
        if (DicesValue != null && DicesValue.Length > 0)
        {
            LocalGameView.SetBtn_State(ACTION_TYPE.MOVE, true);
        }
    }
    #endregion

    #region Movement Controller  
    /// <summary>
    /// Order the current player to Move
    /// </summary>
    private void Move_CurrentPlayer()
    {
        int _moveValue = DicesValue[0];
        int _userToMove = currentPlayerId;

        //Deactive all Buttons so the player cannot stack any action until the move end.
        LocalGameView.SetBtn_State(ACTION_TYPE.ROLL_DICE, false);
        LocalGameView.SetBtn_State(ACTION_TYPE.MOVE, false);
        LocalGameView.SetBtn_State(ACTION_TYPE.END_TURN, false);

        MovePlayer(_moveValue, _userToMove);
    }
    /// <summary>
    /// Order the player to Move
    /// </summary>
    /// <param name="_moveValue">The number of Math to move</param>
    /// <param name="_userToMove">Id of the user to Move in the Players List</param>
    void MovePlayer(int _moveValue, int _userToMove)
    {
        GraphNode currentNode = board.GetCurrentNodeByAddress(GetUserAddress(_userToMove));
        List<GraphNode> targetNodes = board.GetNodesByStep(currentNode, _moveValue);

        if(targetNodes != null && targetNodes.Count > 0)
        {
            _targetList = new Vector3[targetNodes.Count];
            for(int i = 0; i < targetNodes.Count; i++)
            {
                _targetList[i] = targetNodes[i].GetNodeWorldPosition();
            }

            currentTarget = 0;

            //Register Move Action for this turn
            MovementController.OnStartMoving = null;
            MovementController.OnStartMoving += StartMoving1Math;
            MovementController.OnEndMoving = null;
            MovementController.OnEndMoving += EndMoving1Math;
            MoveTheObject();
        }
        else
        {
            return;
        }
    }

    int currentTarget = 0;
    Vector3[] _targetList;
    float timeToTarget = 0.5f;

    /// <summary>
    /// Move the object to the targets in the _targetList. At the last target, when the move action finish, end the Turn base Action
    /// </summary>
    void MoveTheObject()
    {
        if(currentTarget < _targetList.Length)
        {
            MovementController.SetTarget(_targetList[currentTarget], timeToTarget);
            currentTarget++;
        }
        else
        {
            TurnBaseController.EndAction();
        }
    }

    private void StartMoving1Math()
    {
        Debug.Log("[StartMoving1Math] On Move Start: " + currentTarget);
    }

    private void EndMoving1Math()
    {
        Debug.Log("[EndMoving1Math] On Move End: " + currentTarget);

        MoveTheObject();
    }

    private void EndMovingAllTarget()
    {
        LocalGameView.SetBtn_State(ACTION_TYPE.END_TURN, true);
    }
    #endregion

    #region User Controller
    public string GetUserAddress(int _userLocalId)
    {
        if(_userLocalId > 0 
            && _userLocalId < players.Count
            && players[_userLocalId] != null)
        {
            return players[_userLocalId].UserData.Address;
        }
        else
        {
            return "";
        }
    }

    /// <summary>
    /// Initialize the Players Data + Their Actions
    /// </summary>
    private void InitPlayers()
    {
        //Set Player id (The turn order) + Set the user Action
        for (int i = 0; i < players.Count; i++)
        {
            User currentUserData = GetUser(i);
            players[i].Init(currentUserData, TurnBaseController);
            TurnBaseController.Register(players[i]);

            players[i].OnStartRollingDice = null;
            players[i].OnStartRollingDice += (x) => RollDice();

            players[i].OnEndRollingDice = null;
            players[i].OnEndRollingDice += (x) => End_RollDice();

            players[i].OnStartMoving = null;
            players[i].OnStartMoving += (x) => Move_CurrentPlayer();

            players[i].OnEndMoving = null;
            players[i].OnEndMoving += (x) => EndMovingAllTarget();
        }
    }
    #endregion
}
