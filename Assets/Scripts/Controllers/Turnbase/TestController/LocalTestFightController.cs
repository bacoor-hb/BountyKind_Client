using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public delegate void OnRenderQueue(int[] arr);
    public static event OnRenderQueue onRenderQueue;
    public delegate void OnReceiveBattleDatas(List<BattleData> battleDatas, int playerId);
    public static event OnReceiveBattleDatas onReceiveBattleDatas;
    [SerializeField]
    private GameObject prefabPet;

    [SerializeField]
    private GameObject prefabMonster;

    [SerializeField]
    private GameObject unitInQueue;

    [SerializeField]
    private static List<GameObject> unitsInQueue;

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
    private GameObject plane;
    [SerializeField]
    private List<GameObject> spawnPosPets;
    [SerializeField]
    private List<GameObject> spawnPosEnemies;
    [SerializeField]
    private Button FightBTN;
    [SerializeField]
    private Button EndTurnBTN;
    private string currentUnitId;
    private int currentTurn;

    // Start is called before the first frame update
    void Start()
    {
        List<CharacterPosition> charactersPosition = new List<CharacterPosition>();
        charactersPosition.Add(new CharacterPosition("id1", 0, "YOUR_PET"));
        charactersPosition.Add(new CharacterPosition("id2", 1, "YOUR_PET"));
        charactersPosition.Add(new CharacterPosition("id3", 0, "OPPONENT_PET"));
        charactersPosition.Add(new CharacterPosition("id4", 1, "OPPONENT_PET"));

        int[] arr = { 1, 2, 3, 4 };
        onRenderQueue?.Invoke(arr);

        for (var i = 0; i < charactersPosition.Count; i++)
        {
            int characterPostion = charactersPosition[i].position;
            if (charactersPosition[i].side == "YOUR_PET")
            {

                Vector3 position = spawnPosPets[characterPostion].GetComponent<Transform>().position;
                Vector3 pos = new Vector3(position.x, position.y, position.z);
                players[0].AddUnit(players[0].RenderUnit(prefabPet, pos, players[0].transform), charactersPosition[i].id);
            }
            else
            {
                Vector3 position = spawnPosEnemies[characterPostion].GetComponent<Transform>().position;
                Vector3 pos = new Vector3(position.x, position.y, position.z);
                players[0].AddUnit(players[0].RenderUnit(prefabMonster, pos, players[0].transform), charactersPosition[i].id);
            }
        }
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
        FightActionTest.endFightAction += HandleEndFightAction;
        UnitQueueController.onEndQueue += HandleEndQueue;
        turnBaseController.StartGame();
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

    void InitTurn()
    {

    }

    void HandleStartFight(string unitId, int turn)
    {
        if (unitsInQueue.Count > 0)
        {
            currentUnitId = unitId;
            currentTurn = turn;
            for (var i = 0; i < unitsInQueue.Count; i++)
            {
                if (unitsInQueue[i].GetComponent<UnitQueueController>().id != unitId)
                {
                    unitsInQueue[i].GetComponent<UnitQueueController>().SetEndPosition(unitsInQueue[i - 1].transform.position);
                }
                else
                {
                    unitsInQueue[i].GetComponent<UnitQueueController>().SetEndPosition(new Vector3(-180, 0, 0));
                    unitsInQueue[i].GetComponent<UnitQueueController>().SetIsCurrentTarget();
                }
            }
            unitsOutQueue.Add(unitsInQueue[0]);
            unitsInQueue.RemoveAt(0);
        }
        else
        {
            unitsInQueue = unitsOutQueue;
            currentTurn = turn;
            unitsOutQueue = new List<GameObject>();
            for (var i = 0; i < unitsInQueue.Count; i++)
            {
                unitsInQueue[i].GetComponent<UnitQueueController>().ResetPosition();
            }
            currentUnitId = unitId;
            currentTurn = turn;
            for (var i = 0; i < unitsInQueue.Count; i++)
            {
                if (unitsInQueue[i].GetComponent<UnitQueueController>().id != unitId)
                {
                    unitsInQueue[i].GetComponent<UnitQueueController>().SetEndPosition(unitsInQueue[i - 1].transform.position);
                }
                else
                {
                    unitsInQueue[i].GetComponent<UnitQueueController>().SetEndPosition(new Vector3(-180, 0, 0));
                    unitsInQueue[i].GetComponent<UnitQueueController>().SetIsCurrentTarget();
                }
            }
            unitsOutQueue.Add(unitsInQueue[0]);
            unitsInQueue.RemoveAt(0);
        }
    }

    void HandleEndQueue()
    {
        currentUnitInQueue.transform.Find("Avatar").GetComponent<Image>().sprite = unitsOutQueue[unitsOutQueue.Count - 1].transform.Find("Background").transform.Find("Avatar").GetComponent<Image>().sprite;
        bigCurrentUnit.transform.Find("Avatar").GetComponent<Image>().sprite = unitsOutQueue[unitsOutQueue.Count - 1].transform.Find("Background").transform.Find("Avatar").GetComponent<Image>().sprite;
    }

    void HandleEndFightAction(string unitId)
    {

    }

    void HandleEndTurn()
    {
        var currentPlayer = players[turnBaseController.CurrentPlayer];
        turnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.END_TURN));
    }
    void InsertFakeData()
    {
        List<BattleData> battleDatas = new List<BattleData>();
        BattleUnit currentUnit1 = new BattleUnit("id1", 100);
        BattleUnit targetUnit1 = new BattleUnit("id3", 90);
        BattleUnit currentUnit2 = new BattleUnit("id2", 100);
        BattleUnit targetUnit2 = new BattleUnit("id4", 80);

        BattleUnit currentUnit3 = new BattleUnit("id3", 100);
        BattleUnit targetUnit3 = new BattleUnit("id1", 90);
        BattleUnit currentUnit4 = new BattleUnit("id4", 100);
        BattleUnit targetUnit4 = new BattleUnit("id2", 80);

        battleDatas.Add(new BattleData(currentUnit1, targetUnit1, "YOUR_PET", 1));
        battleDatas.Add(new BattleData(currentUnit2, targetUnit2, "YOUR_PET", 1));
        battleDatas.Add(new BattleData(currentUnit3, targetUnit3, "OPPONENT_PET", 1));
        battleDatas.Add(new BattleData(currentUnit4, targetUnit4, "OPPONENT_PET", 1));

        battleDatas.Add(new BattleData(currentUnit1, targetUnit1, "YOUR_PET", 2));
        battleDatas.Add(new BattleData(currentUnit2, targetUnit2, "YOUR_PET", 2));
        battleDatas.Add(new BattleData(currentUnit3, targetUnit3, "OPPONENT_PET", 2));
        battleDatas.Add(new BattleData(currentUnit4, targetUnit4, "OPPONENT_PET", 2));


        onReceiveBattleDatas(battleDatas, turnBaseController.CurrentPlayer);
        var currentPlayer = players[turnBaseController.CurrentPlayer];
        for (var i = 0; i < battleDatas.Count; i++)
        {
            turnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.FIGHT));
        }
    }
}
