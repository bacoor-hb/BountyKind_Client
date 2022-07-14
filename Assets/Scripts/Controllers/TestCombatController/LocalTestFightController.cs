using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CombatProcess;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class UnitQueue
{
    public string id;
    public string baseKey;
    public int turn;
    public UnitQueue(string _id, string _baseKey, int _turn)
    {
        id = _id;
        baseKey = _baseKey;
        turn = _turn;
    }
}
class CharacterPosition
{
    public string id;
    public int position;
    public string side;
    public CharacterPosition(string _id, int _position, string _side)
    {
        id = _id;
        position = _position;
        side = _side;
    }
}

public class LocalTestFightController : MonoBehaviour
{
    public APIManager apiManager;
    public delegate void OnEventTriggered();
    public static event OnEventTriggered OnSetUserInfomationCompleted;
    public static event OnEventTriggered OnSetOpponentInfomationCompleted;
    public static event OnEventTriggered OnSetAllCharactersPositionCompleted;
    public static event OnEventTriggered OnSetBattleDataCompleted;
    public delegate void OnRenderQueue(List<UnitQueue> arr);
    public static event OnRenderQueue onRenderQueue;
    public delegate void OnReceiveBattleDatasEvent(List<BattleProgess> battleData, int playerId);
    public static event OnReceiveBattleDatasEvent OnReceiveBattleDatas;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private List<GameObject> characterPrefabs;

    [SerializeField]
    public List<GameObject> players;

    [SerializeField]
    private TurnBaseController turnBaseController;
    [SerializeField]
    private BattleData battleData;
    [SerializeField]
    private LocalViewManager localViewManager;
    [SerializeField]
    private QueueController queueController;
    [SerializeField]
    private List<FormationCharacters> yourCharacters;
    [SerializeField]
    private List<FormationCharacters> opponentCharacters;

    // Start is called before the first frame update
    public void Init()
    {
        yourCharacters = new();
        opponentCharacters = new();
        localViewManager.buttonViewManager.FightButton.onClick.AddListener(() => { GetFakeData(); });
        QueueViewManager.OnRenderQueueCompleted += HandleOnRenderQueueCompleted;
        FightActionTest.onEndTurnFights += HandleEndTurnFights;
        FightActionTest.startFight += HandleStartFight;
        PlayerTestFightController.OnClearCharacter += HandleOnClearCharacter;
        UnitQueueController.OnEndQueue += HandleEndQueue;
        TurnBaseController.OnStartTurn += HandleStartTurn;
        OnSetUserInfomationCompleted += GetOpponentsFormation;
        OnSetOpponentInfomationCompleted += SetAllCharactersPosition;
        OnSetAllCharactersPositionCompleted += HandleOnSetAllCharactersPositionCompleted;
        OnSetBattleDataCompleted += HandleOnSetBattleDataCompleted;
        localViewManager.resultViewManager.continueButton.onClick.AddListener(() => { HandleContinue(); });
        apiManager.OnGetFormationFinished += HandleGetFormationFinished;
        turnBaseController.Init();
        players = new List<GameObject>();
        GameObject player = Instantiate(playerPrefab, transform);
        players.Add(player);
        for (int i = 0; i < players.Count; i++)
        {
            PlayerTestFightController playerController = players[i].GetComponent<PlayerTestFightController>();
            playerController.InitPlayer(playerController.id, turnBaseController);
            turnBaseController.Register(playerController);
        }
        turnBaseController.StartGame();
    }
    public void ProcessCharactersPosition()
    {
        string uri = "https://dev-game-api.w3w.app/api/users/formation";
        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI2MWVhNjhiMjk2YjJkMTNjMGVlZTcxYTMiLCJhZGRyZXNzIjoiMHg2NDQ3MGU1ZjVkZDM4ZTQ5NzE5NGJiY2FmOGRhYTdjYTU3ODkyNmY2IiwidXNlcm5hbWUiOiJDw7RuZyBTVVBFUiBWSVAgUFJPIiwiaW1hZ2UiOiJRbWFwU2ZERXlUUlAzZ3Q4bTd3UkdrQVhyRWE3eUpRZnI2b2FZem9pMnphb01iIiwiaXNSZWNlaXZlZCI6dHJ1ZSwiaXNOZXdVc2VyIjpmYWxzZSwiaWF0IjoxNjU3MTY4MTQ0LCJleHAiOjE5NzI3NDQxNDR9.c4mqy9ygQJHuJ7J1ZB0FhVe6SXwjYGjHfqgQ095mwYk";
        StartCoroutine(apiManager.GetFormation(uri, token));
    }
    public void GetOpponentsFormation()
    {
        //BountyMap currentMap = GlobalManager.Instance.UserDataManager.GetCurrentMap();
        //int currentNodeIndex = GlobalManager.Instance.UserDataManager.UserGameStatus.currentNode;
        //MapNode currentNode = currentMap.nodes[currentNodeIndex];
        //List<Enemy> enemies = currentNode.enemies;
        //for (int i = 0; i < enemies.Count; i++)
        //{
        //    FormationCharacters character = new();
        //    character._id = enemies[i]._id;
        //    character.baseKey = "default_character_3";
        //    character.position = enemies[i].position;
        //    character.hp = enemies[i].hp;
        //    character.atk = enemies[i].atk;
        //    character.speed = enemies[i].speed;
        //    character.def = enemies[i].def;
        //    character.level = enemies[i].level;
        //    opponentCharacters.Add(character);
        //}
        GetFakeOpponentData();
        OnSetOpponentInfomationCompleted?.Invoke();
    }

    void GetFakeOpponentData()
    {
        FormationCharacters character3 = new();
        character3._id = "627a348d2a246f967f142ebd";
        character3.baseKey = "default_character_3";
        character3.position = 0;
        character3.hp = 100;
        character3.atk = 22;
        character3.speed = 5;
        character3.def = 23;
        character3.level = 7;
        FormationCharacters character4 = new FormationCharacters();
        character4._id = "id8";
        character4.baseKey = "default_character_4";
        character4.position = 1;
        character4.hp = 100;
        character4.atk = 44;
        character4.speed = 2;
        character4.def = 44;
        character4.level = 4;
        opponentCharacters.Add(character3);
    }
    void Start()
    {
        Init();
        InsertBattleData(GetFakeData());
        ProcessCharactersPosition();
    }

    void HandleStartTurn(int currentPlayer)
    {

    }

    void HandleContinue()
    {
        GlobalManager.Instance.LoadingManager.LoadWithLoadingScene(SCENE_NAME.GameScene);
    }

    void HandleGetFormationFinished(FormationCharacters[] userCharacters)
    {
        for (int i = 0; i < userCharacters.Length; i++)
        {
            yourCharacters.Add(userCharacters[i]);
        }
        OnSetUserInfomationCompleted?.Invoke();
    }

    void HandleOnSetAllCharactersPositionCompleted()
    {
        //StartCoroutine(GetData());
        ProcessBattleData();
    }

    void HandleOnSetBattleDataCompleted()
    {
        ProcessBattleData();
    }


    public void SetAllCharactersPosition()
    {
        PlayerTestFightController playerController = players[0].GetComponent<PlayerTestFightController>();
        List<GameObject> spawnPosPets = localViewManager.boardViewManager.spawnPosPets;
        List<GameObject> spawnPosEnemies = localViewManager.boardViewManager.spawnPosEnemies;
        for (var i = 0; i < yourCharacters.Count; i++)
        {
            int characterPosition = yourCharacters[i].position;
            string characterId = yourCharacters[i]._id;
            string characterBaseKey = yourCharacters[i].baseKey;
            GameObject myGameObj = characterPrefabs.Find(prefab => prefab.name == characterBaseKey);
            GameObject instantiateObj = localViewManager.boardViewManager.RenderUnitToBoard(myGameObj, characterPosition, "pet");
            instantiateObj.GetComponent<UnitController>().characterInfo = yourCharacters[i];
            playerController.AddUnit(instantiateObj, characterId);
        }

        for (var z = 0; z < opponentCharacters.Count; z++)
        {
            int characterPosition = opponentCharacters[z].position;
            string characterId = opponentCharacters[z]._id;
            string characterBaseKey = opponentCharacters[z].baseKey;
            GameObject myGameObj = characterPrefabs.Find(prefab => prefab.name == characterBaseKey);
            GameObject instantiateObj = localViewManager.boardViewManager.RenderUnitToBoard(myGameObj, characterPosition, "enemy");
            instantiateObj.GetComponent<UnitController>().characterInfo = opponentCharacters[z];
            playerController.AddUnit(instantiateObj, characterId);
        }
        OnSetAllCharactersPositionCompleted?.Invoke();
    }

    public void HandleOnRenderQueueCompleted(List<GameObject> _unitsInQueue)
    {
        queueController.SetUnitsInQueue(_unitsInQueue);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void HandleEndTurnFights()
    {
        turnBaseController.EndTurn();
        int batteStatus = battleData.status;
        StartCoroutine(localViewManager.resultViewManager.DisplayResultPanel(batteStatus));
    }

    void InitTurn(List<UnitQueue> unitQueues)
    {
        onRenderQueue?.Invoke(unitQueues);
    }

    void HandleStartFight(string unitId, int turn)
    {
        localViewManager.turnViewManager.SetTurnText(turn);
        for (int i = 0; i < queueController.unitsInQueue.Count; i++)
        {
            int index = i;
            UnitQueueController currentUnitInQueue = queueController.unitsInQueue[index].GetComponent<UnitQueueController>();
            if (currentUnitInQueue.id == unitId && currentUnitInQueue.turn == turn)
            {
                queueController.unitsInQueue[i].GetComponent<UnitQueueController>().SetEndPosition(localViewManager.queueViewManager.endObj.transform.position);
                queueController.unitsInQueue[i].GetComponent<UnitQueueController>().SetIsCurrentTarget();
            }
            else
            {
                queueController.unitsInQueue[i].GetComponent<UnitQueueController>().SetEndPosition(queueController.unitsInQueue[i - 1].transform.position);
            }
        }
        queueController.unitsOutQueue.Add(queueController.unitsInQueue[0]);
        queueController.unitsInQueue.RemoveAt(0);
    }

    void HandleEndQueue()
    {
        List<GameObject> unitsOutQueue = queueController.unitsOutQueue;
        GameObject _currentUnitObj = unitsOutQueue[unitsOutQueue.Count - 1];
        string currentUnitId = _currentUnitObj.GetComponent<UnitQueueController>().id;
        FormationCharacters currentUnit = players[0].GetComponent<PlayerTestFightController>().characters[currentUnitId].GetComponent<UnitController>().characterInfo;
        localViewManager.queueViewManager.SetCurrentUnit(unitsOutQueue);
        localViewManager.currentUnitViewManager.SetCurretUnitView(currentUnit);
    }

    BattleData GetFakeData()
    {
        BattleData _battleData = new BattleData();
        _battleData.skip = false;
        _battleData.status = 1;
        _battleData.battleProgress = new List<BattleProgess>();

        BattleProgess battleProgess1 = new BattleProgess();
        battleProgess1.turn = 1;
        battleProgess1.order = 1;
        battleProgess1.type = "Attack";
        battleProgess1.attacker = new BattleUnit(30, 30, 30, 30, "62bc7e36d47e20163c0447ad", "OurSide");
        battleProgess1.target = new BattleUnit(10, 10, 0, 20, "627a348d2a246f967f142ebd", "OpposingSide");

        BattleProgess battleProgess2 = new BattleProgess();
        battleProgess2.turn = 1;
        battleProgess2.order = 2;
        battleProgess2.type = "Attack";
        battleProgess2.attacker = new BattleUnit(15, 15, 15, 15, "62bbf94f59aa4666f6d19b33", "OurSide");
        battleProgess2.target = new BattleUnit(10, 10, 0, 15, "627a348d2a246f967f142ebd", "OpposingSide");

        BattleProgess battleProgess3 = new BattleProgess();
        battleProgess3.turn = 2;
        battleProgess3.order = 1;
        battleProgess3.type = "Attack";
        battleProgess3.attacker = new BattleUnit(12, 14, 13, 10, "62bbf90659aa4666f6d19b1a", "OurSide");
        battleProgess3.target = new BattleUnit(10, 10, 0, 0, "627a348d2a246f967f142ebd", "OpposingSide");
        _battleData.battleProgress.Add(battleProgess1);
        _battleData.battleProgress.Add(battleProgess2);
        _battleData.battleProgress.Add(battleProgess3);
        return _battleData;
    }

    public void InsertBattleData(BattleData _battleData)
    {
        battleData = _battleData;
    }

    void ProcessBattleData()
    {
        List<UnitQueue> unitQueues = new();
        Debug.Log(battleData.battleProgress.Count);
        PlayerTestFightController playerController = players[0].GetComponent<PlayerTestFightController>();
        for (var i = 0; i < battleData.battleProgress.Count; i++)
        {
            string id = battleData.battleProgress[i].attacker._id;
            int turn = battleData.battleProgress[i].turn;
            string baseKey = playerController.characters[id].GetComponent<UnitController>().characterInfo.baseKey;
            unitQueues.Add(new UnitQueue(id, baseKey, turn));
        }
        InitTurn(unitQueues);
        OnReceiveBattleDatas?.Invoke(battleData.battleProgress, turnBaseController.CurrentPlayer);
        var currentPlayer = players[turnBaseController.CurrentPlayer].GetComponent<PlayerTestFightController>();
        for (var i = 0; i < battleData.battleProgress.Count; i++)
        {
            turnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.COMBAT));
        }
    }

    void HandleOnClearCharacter(GameObject characterObj)
    {
        localViewManager.boardViewManager.ClearCharacterOnboard(characterObj);
    }

    IEnumerator GetData()
    {
        Startup battleStartUp = new Startup();
        CombatProcess.InputType myInput = new();
        CombatProcess.FormationCharacters[] userCharactersArr = JsonConvert.DeserializeObject<CombatProcess.FormationCharacters[]>(JsonConvert.SerializeObject(yourCharacters));
        CombatProcess.FormationCharacters[] opponentCharactersArr = JsonConvert.DeserializeObject<CombatProcess.FormationCharacters[]>(JsonConvert.SerializeObject(opponentCharacters));

        myInput._userCharacters = userCharactersArr;
        myInput._opponentCharacters = opponentCharactersArr;

        Task<object> task = battleStartUp.Invoke(JsonConvert.SerializeObject(myInput)); ;
        yield return new WaitUntil(() => task.IsCompleted);
        try
        {
            object result = task.Result;
            battleData = JsonConvert.DeserializeObject<BattleData>(JsonConvert.SerializeObject(result));
            OnSetBattleDataCompleted?.Invoke();
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex);
        }
    }
}
