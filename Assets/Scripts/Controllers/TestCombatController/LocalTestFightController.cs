using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitQueue
{
    public string id;
    public int turn;
    public UnitQueue(string _id, int _turn)
    {
        id = _id;
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
    public delegate void OnRenderQueue(List<UnitQueue> arr);
    public static event OnRenderQueue onRenderQueue;
    public delegate void OnReceiveBattleDatas(List<BattleData> battleDatas, int playerId);
    public static event OnReceiveBattleDatas onReceiveBattleDatas;
    [SerializeField]
    private List<GameObject> characterPrefabs;
    [SerializeField]
    private GameObject prefabPet;

    [SerializeField]
    private GameObject prefabMonster;

    [SerializeField]
    private static List<GameObject> unitsInQueue = new List<GameObject>();

    [SerializeField]
    private List<GameObject> unitsOutQueue;


    [SerializeField]
    private GameObject currentUnitInQueue;

    [SerializeField]
    private GameObject bigCurrentUnit;

    [SerializeField]
    public List<PlayerTestFightController> players;

    [SerializeField]
    private TurnBaseController turnBaseController;

    [SerializeField]
    private List<GameObject> spawnPosPets;
    [SerializeField]
    private List<GameObject> spawnPosEnemies;
    [SerializeField]
    private Button FightBTN;
    [SerializeField]
    private Button EndTurnBTN;
    private List<BattleData> battleDatas = new List<BattleData>();

    // Start is called before the first frame update
    void Start()
    {
        List<Character> yourCharacters = new List<Character>();
        List<Character> opponentCharacters = new List<Character>();

        Character character1 = new Character();
        character1.id = "id1";
        character1.position = 0;
        Character character2 = new Character();
        character2.id = "id2";
        character2.position = 1;
        Character character3 = new Character();
        character3.id = "id7";
        character3.position = 0;
        Character character4 = new Character();
        character4.id = "id8";
        character4.position = 1;

        yourCharacters.Add(character1);
        yourCharacters.Add(character2);
        opponentCharacters.Add(character3);
        opponentCharacters.Add(character4);

        SetAllCharactersPosition(yourCharacters, opponentCharacters);

        turnBaseController.Init();
        //Set Player id ~ The turn order
        for (int i = 0; i < players.Count; i++)
        {
            players[i].InitPlayer(players[i].id, turnBaseController);
            turnBaseController.Register(players[i]);
        }
        FightBTN.onClick.AddListener(() => { InsertFakeData(); });
        EndTurnBTN.onClick.AddListener(() => { HandleEndTurn(); });
        FightActionTest.onEndTurnFights += HandleEndTurnFights;
        FightActionTest.startFight += HandleStartFight;
        UnitQueueController.onEndQueue += HandleEndQueue;
        turnBaseController.StartGame();
    }

    public void SetAllCharactersPosition(List<Character> yourCharacters, List<Character> opponentCharacters)
    {
        for (var i = 0; i < yourCharacters.Count; i++)
        {
            int characterPostion = yourCharacters[i].position;
            Vector3 position = spawnPosPets[characterPostion].GetComponent<Transform>().position;
            Vector3 pos = new Vector3(position.x, position.y, position.z);
            GameObject myGameObj = characterPrefabs.Find(prefab => prefab.name == yourCharacters[i].id);
            players[0].AddUnit(players[0].RenderUnit(myGameObj, pos, players[0].transform), yourCharacters[i].id);
        }

        for (var i = 0; i < opponentCharacters.Count; i++)
        {
            int characterPostion = opponentCharacters[i].position;
            Vector3 position = spawnPosEnemies[characterPostion].GetComponent<Transform>().position;
            Vector3 pos = new Vector3(position.x, position.y, position.z);
            GameObject myGameObj = characterPrefabs.Find(prefab => prefab.name == opponentCharacters[i].id);
            players[0].AddUnit(players[0].RenderUnit(myGameObj, pos, players[0].transform), opponentCharacters[i].id);

        }
    }

    public static void SetUnitQueueList(List<GameObject> _unitsInQueue)
    {
        unitsInQueue = _unitsInQueue;

    }

    // Update is called once per frame
    void Update()
    {
        var currentPlayer = players[turnBaseController.CurrentPlayer];
    }

    void HandleEndTurnFights()
    {
        turnBaseController.EndTurn();
    }

    void InitTurn(List<UnitQueue> unitQueues)
    {
        onRenderQueue?.Invoke(unitQueues);
    }

    void HandleStartFight(string unitId, int turn)
    {
        for (var i = 0; i < unitsInQueue.Count; i++)
        {
            UnitQueueController currentUnitInQueue = unitsInQueue[i].GetComponent<UnitQueueController>();
            if (currentUnitInQueue.id == unitId && currentUnitInQueue.turn == turn)
            {
                unitsInQueue[i].GetComponent<UnitQueueController>().SetEndPosition(new Vector3(-180, 0, 0));
                unitsInQueue[i].GetComponent<UnitQueueController>().SetIsCurrentTarget();
            }
            else
            {
                unitsInQueue[i].GetComponent<UnitQueueController>().SetEndPosition(unitsInQueue[i - 1].transform.position);
            }
        }
        unitsOutQueue.Add(unitsInQueue[0]);
        unitsInQueue.RemoveAt(0);
    }

    void HandleEndQueue()
    {
        currentUnitInQueue.transform.Find("Avatar").GetComponent<Image>().sprite = unitsOutQueue[unitsOutQueue.Count - 1].transform.Find("Background").transform.Find("Avatar").GetComponent<Image>().sprite;
        bigCurrentUnit.transform.Find("Avatar").GetComponent<Image>().sprite = unitsOutQueue[unitsOutQueue.Count - 1].transform.Find("Background").transform.Find("Avatar").GetComponent<Image>().sprite;
    }

    void HandleEndTurn()
    {
        var currentPlayer = players[turnBaseController.CurrentPlayer];
        turnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.END_TURN));
    }
    void InsertFakeData()
    {
        battleDatas = new List<BattleData>();
        BattleUnit currentUnit1 = new BattleUnit("id1", 100);
        BattleUnit targetUnit1 = new BattleUnit("id7", 90);
        BattleUnit currentUnit2 = new BattleUnit("id2", 100);
        BattleUnit targetUnit2 = new BattleUnit("id8", 80);

        BattleUnit currentUnit3 = new BattleUnit("id7", 100);
        BattleUnit targetUnit3 = new BattleUnit("id1", 90);
        BattleUnit currentUnit4 = new BattleUnit("id8", 100);
        BattleUnit targetUnit4 = new BattleUnit("id2", 80);

        battleDatas.Add(new BattleData(currentUnit1, targetUnit1, 1));
        battleDatas.Add(new BattleData(currentUnit2, targetUnit2, 1));
        battleDatas.Add(new BattleData(currentUnit3, targetUnit3, 1));
        battleDatas.Add(new BattleData(currentUnit4, targetUnit4, 1));

        battleDatas.Add(new BattleData(currentUnit1, targetUnit1, 2));
        battleDatas.Add(new BattleData(currentUnit2, targetUnit2, 2));
        battleDatas.Add(new BattleData(currentUnit3, targetUnit3, 2));
        battleDatas.Add(new BattleData(currentUnit4, targetUnit4, 2));
        List<UnitQueue> unitQueues = new List<UnitQueue>();
        for (var i = 0; i < battleDatas.Count; i++)
        {
            string id = battleDatas[i].currentUnit.id;
            int turn = battleDatas[i].turn;
            unitQueues.Add(new UnitQueue(id, turn));
        }
        InitTurn(unitQueues);
        onReceiveBattleDatas(battleDatas, turnBaseController.CurrentPlayer);
        var currentPlayer = players[turnBaseController.CurrentPlayer];
        for (var i = 0; i < battleDatas.Count; i++)
        {
            turnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.COMBAT));
        }
    }
}
