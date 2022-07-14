using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LocalGameController : MonoBehaviour
{
    [SerializeField]
    private List<UserActionLocalManager> players;

    [Header ("Event Controller")]
    [SerializeField]
    private Multiplayer_GameEventController GameEventController;

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

    [Header("Network Event Control")]
    [SerializeField]
    private Multiplayer_GameEventController Multiplayer_GameEvent;

    private UserDataManager UserDataManager;
    private NetworkManager NetworkManager;

    private GameRoomSchema currentRoom;
    //-------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------
    #region Initialize
    // Start is called before the first frame update
    void Start()
    {
        UserDataManager = GlobalManager.Instance.UserDataManager;
        NetworkManager = GlobalManager.Instance.NetworkManager;

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
        Init_Board();

        //Init Game Event Controller
        GameEventController.Init();

        //Init the Multiplayer Game Event Controller
        Init_GameEvent();

        //Update the User Data View
        Init_LocalView();
        Multiplayer_GameEvent.HandleUpdateBalance();
    }

    /// <summary>
    /// Init the Board
    /// </summary>
    private void Init_Board()
    {
        board.Init();
        board.GenerateBoard(nodeList);

        //Register all users to the Map        
        string[] usersAddress = new string[players.Count];
        GraphNode[] graphNodes = new GraphNode[players.Count];
        currentRoom = NetworkManager.GetRoomState();

        //Initialize the player on the board
        for (int i = 0; i < players.Count; i++)
        {
            usersAddress[i] = GetUserAddress(i);
            int currentNodeId = Mathf.RoundToInt(currentRoom.players[i].currentNode);
            Debug.Log("[LocalGameController] Init user position: " + currentNodeId);
            GraphNode userPos = board.GetNode(currentNodeId);
            graphNodes[i] = userPos;

            MovementController.Teleport(userPos.transform);
        }
        board.InitUserNodeList(usersAddress, graphNodes);
    }

    /// <summary>
    /// Init the Multiplayer Game Event Controller
    /// </summary>
    private void Init_GameEvent()
    {
        Multiplayer_GameEvent.OnRollResultReturn = null;
        Multiplayer_GameEvent.OnRollResultReturn += RollDice;
        Multiplayer_GameEvent.OnBalanceReturn = null;
        Multiplayer_GameEvent.OnBalanceReturn += LocalGameView.UpdateBalance;
        Multiplayer_GameEvent.OnChanceReturn = null;
        Multiplayer_GameEvent.OnChanceReturn += Chance_GetReward;
        Multiplayer_GameEvent.OnLuckyDrawReturn = null;
        Multiplayer_GameEvent.OnLuckyDrawReturn += GetReward_LuckyDraw;
        Multiplayer_GameEvent.OnBattleReturn = null;
        Multiplayer_GameEvent.OnBattleReturn += Combat_GetData;
    }

    /// <summary>
    /// Update the User Data View
    /// </summary>
    private void Init_LocalView()
    {
        LocalGameView.UpdateUserData(UserDataManager.UserData);

        LocalGameView.Init();
        LocalGameView.RollDiceView.OnEngageBtnPressed = null;
        LocalGameView.RollDiceView.OnEngageBtnPressed += RollDice_Action;

        LocalGameView.ChanceView.OnEngageBtnPressed = null;
        LocalGameView.ChanceView.OnEngageBtnPressed += Chance_Action;

        LocalGameView.LuckyDrawPopup.OnLuckyDraw_Engage = null;
        LocalGameView.LuckyDrawPopup.OnLuckyDraw_Engage += LuckyDraw_Action;

        LocalGameView.CombatGameView.OnEngageBtnPressed = null;
        LocalGameView.CombatGameView.OnEngageBtnPressed += Combat_Action;

        LocalGameView.QuitBoard_Btn.onClick.RemoveAllListeners();
        LocalGameView.QuitBoard_Btn.onClick.AddListener(Multiplayer_GameEvent.HandleQuitBoard);
    }
    #endregion
    //-------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------
    #region Turn Base Controller
    int rollNumber = 0;
    UserActionLocalManager currentPlayer;
    int currentPlayerId;
    void OnStartTurn(int userID)
    {
        //Get Current Player
        currentPlayerId = TurnBaseController.CurrentPlayer;
        currentPlayer = players[currentPlayerId];

        //User has 1 roll Number and 1 move number at each turn
        rollNumber = 1;

        //Reset Dice
        DicesController.ResetDicesPosition();

        //Init the View + Button Event, Only for local player.        
        LocalGameView.UpdateUserData(UserDataManager.UserData);
        LocalGameView.DeactiveAllPopup();

        //Update current User's current Node
        UserDataManager.UserGameStatus.currentNode = Mathf.RoundToInt(currentRoom.players[currentPlayerId].currentNode);
        var actionType = UserDataManager.UserGameStatus.Get_MapActionType();
        Debug.Log("[LocalGameController] [OnStartTurn] Current Math status: " + currentRoom.players[currentPlayerId].isInteracted);
        
        //Face the Avatar to the right direction
        Vector3 currentPos = nodeList[UserDataManager.UserGameStatus.currentNode].transform.position;
        var nextNodeList = UserDataManager.GetNextNodes(UserDataManager.UserGameStatus.currentNode);
        Vector3 nextPos = nodeList[nextNodeList[0]].transform.position;
        Vector3 dir = nextPos - currentPos;
        MovementController.FaceToDir(dir);
        Debug.Log("CurrentPos: "+ currentPos + " | Next Pos:" + nextPos);

        //If the current player is not yet interracted with the current node.
        if (!currentRoom.players[currentPlayerId].isInteracted)
        {
            OnTriggerMathEffect();
        }
        else
        {
            LocalGameView.SetState_RollDicePopup(true);
        }
    }

    /// <summary>
    /// Add Roll Dice Action to Queue.
    /// </summary>
    void RollDice_Action(bool _skip)
    {
        Debug.Log("[LocalGameController][RollDice_Action] RollDice_Action: " + _skip);
        finishRollPopupAnim = false;

        currentPlayer.SetSkipRollDice(_skip);
        TurnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.ROLL_DICE));       
    }

    /// <summary>
    /// Add Action Move to the Queue
    /// </summary>
    private void Move_Action()
    {
        TurnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.MOVE));
    }

    /// <summary>
    /// Add Action trigger Chance to the Queue
    /// </summary>
    private void Chance_Action(bool _skip)
    {
        Debug.Log("[LocalGameController] Accept Chance...");

        currentPlayer.SetSkipChance(_skip);
        TurnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.CHANCE));             
    }

    /// <summary>
    /// Add Lucky Draw Action to the Queue
    /// </summary>
    private void LuckyDraw_Action(bool _skip)
    {
        Debug.Log("[LocalGameController] Accept Lucky Draw..." + _skip);
        currentPlayer.SetSkipLuckyDraw(_skip);
        TurnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.LUCKY_DRAW));        
    }
    /// <summary>
    /// Add Combat Action to the Queue
    /// </summary>
    private void Combat_Action(bool _skipCombat)
    {
        Debug.Log("[LocalGameController] Accept combat..." + _skipCombat);
        currentPlayer.SetSkipCombat(_skipCombat);
        TurnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.COMBAT));
    }

    /// <summary>
    /// Add End Turn Action to the Queue
    /// </summary>
    private void EndTurn_Action()
    {
        Debug.Log("[LocalGameController] End Turn...");
        TurnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.END_TURN));
    }
    #endregion    
    //-------------------------------------------------------------------------------------------
    //-------------------------------------------------------------------------------------------
    #region User Controller
    public string GetUserAddress(int _userLocalId)
    {
        if(_userLocalId >= 0 
            && _userLocalId < players.Count
            && players[_userLocalId] != null)
        {
            return UserDataManager.GetUserData(_userLocalId).address;
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
            players[i].Init(TurnBaseController);
            TurnBaseController.Register(players[i]);

            players[i].OnStartRollingDice = null;
            players[i].OnStartRollingDice += (x) => Call_RollDice();

            players[i].OnEndRollingDice = null;
            players[i].OnEndRollingDice += (x) => End_RollDice();

            players[i].OnStartMoving = null;
            players[i].OnStartMoving += (x) => Move_CurrentPlayer();

            players[i].OnEndMoving = null;
            players[i].OnEndMoving += (x) => EndMovingAllTarget();

            players[i].OnStartLuckyDraw = null;
            players[i].OnStartLuckyDraw += (x) => Start_LuckyDraw();

            players[i].OnEndLuckyDraw = null;
            players[i].OnEndLuckyDraw += (x) => End_LuckyDraw();

            players[i].OnStartChance = null;
            players[i].OnStartChance += (x) => Start_Chance();

            players[i].OnEndChance = null;
            players[i].OnEndChance += (x) => End_Chance();

            players[i].OnStartCombat = null;
            players[i].OnStartCombat +=  Start_Combat;

            players[i].OnEndCombat = null;
            players[i].OnEndCombat += (x) => End_Combat();
        }
    }

    private void OnTriggerMathEffect()
    {
        LocalGameView.DeactiveAllPopup();

        var actionType = UserDataManager.UserGameStatus.Get_MapActionType();
        Debug.Log("[LocalGameController] OnTriggerMathEffect: " + actionType.ToString());
        switch (actionType)
        {
            case ACTION_TYPE.LUCKY_DRAW:
                //LocalGameView.SetBtn_State(ACTION_TYPE.LUCKY_DRAW, true);
                LocalGameView.SetState_LuckyDrawPopup(true);
                break;
            case ACTION_TYPE.CHANCE:
                //LocalGameView.SetBtn_State(ACTION_TYPE.CHANCE, true);
                LocalGameView.SetState_ChanceView(true);
                break;
            case ACTION_TYPE.COMBAT:
                //LocalGameView.SetBtn_State(ACTION_TYPE.COMBAT, true);
                LocalGameView.SetState_BattleView(true);
                break;
            case ACTION_TYPE.END_TURN:
                Multiplayer_GameEvent.Handle_Other_Default();
                EndTurn_Action();
                //LocalGameView.SetBtn_State(ACTION_TYPE.END_TURN, true);
                break;
            case ACTION_TYPE.INVALID_ACTION:
                Debug.LogError("[LocalGameController] OnTriggerMathEffect ERROR: INVALID ACTION.");
                break;
            default:
                Debug.LogError("[LocalGameController] OnTriggerMathEffect ERROR: STRANGE ACTION: " + actionType.ToString());
                break;
        }
    }
    #endregion
}
