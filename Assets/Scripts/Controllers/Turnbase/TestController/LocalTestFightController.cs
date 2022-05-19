using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalTestFightController : MonoBehaviour
{
    public delegate void OnReceiveBattleDatas(List<BattleData> battleDatas, int playerId);
    public static event OnReceiveBattleDatas onReceiveBattleDatas;
    [SerializeField]
    private GameObject prefabPet;

    [SerializeField]
    private GameObject prefabMonster;

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

    // Start is called before the first frame update
    void Start()
    {
        int[] yourPets =  { 0, 1, 2, 6, 8 };
        int[] opponentPets = { 1, 2, 5, 6, 7 };
        for (var i = 0; i < yourPets.Length; i++)
        {
            Vector3 pos = new Vector3(spawnPosPets[yourPets[i]].GetComponent<Transform>().position.x, spawnPosPets[yourPets[i]].GetComponent<Transform>().position.y, spawnPosPets[yourPets[i]].GetComponent<Transform>().position.z);
            players[0].AddUnit(players[0].RenderUnit(prefabPet, pos, players[0].transform), "YOUR_PET");
        }
        for(var i = 0; i < opponentPets.Length; i++)
        {
            Vector3 pos = new Vector3(spawnPosEnemies[opponentPets[i]].GetComponent<Transform>().position.x, spawnPosEnemies[opponentPets[i]].GetComponent<Transform>().position.y, spawnPosEnemies[opponentPets[i]].GetComponent<Transform>().position.z);
            players[0].AddUnit(players[0].RenderUnit(prefabMonster, pos, players[0].transform), "OPPONENT_PET");
        }
        turnBaseController.Init();
        //Set Player id ~ The turn order
        for (int i = 0; i < players.Count; i++)
        {
            players[i].InitPlayer(players[i].id, turnBaseController);
            turnBaseController.Register(players[i]);
        }
        FightBTN.onClick.AddListener(() => { InsertFakeData();  });
        EndTurnBTN.onClick.AddListener(() => { HandleEndTurn(); });
        FightActionTest.onEndTurnFights += HandleEndTurnFights;
        turnBaseController.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        var currentPlayer = players[turnBaseController.CurrentPlayer];
        if (Input.GetKeyDown(KeyCode.A))
        {
            turnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.FIGHT));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (turnBaseController.IsStarting)
            {
                turnBaseController.EndGame();
            }
            else
            {
                turnBaseController.StartGame();            
            }
        }
    }
    
    void HandleEndTurnFights()
    {
        turnBaseController.EndTurn();
    }

    void HandleEndTurn()
    {
        var currentPlayer = players[turnBaseController.CurrentPlayer];
        turnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.END_TURN));
    }
    void InsertFakeData()
    {
        List<BattleData> battleDatas = new List<BattleData>();
        BattleUnit currentUnit1 = new BattleUnit(0, 100);
        BattleUnit targetUnit1 = new BattleUnit(0, 90);
        BattleUnit currentUnit2 = new BattleUnit(1, 100);
        BattleUnit targetUnit2 = new BattleUnit(0, 80);
        BattleUnit currentUnit3 = new BattleUnit(2, 100);
        BattleUnit targetUnit3 = new BattleUnit(0, 70);
        BattleUnit currentUnit4 = new BattleUnit(0, 100);
        BattleUnit targetUnit4 = new BattleUnit(0, 90);
        BattleUnit currentUnit5 = new BattleUnit(1, 100);
        BattleUnit targetUnit5 = new BattleUnit(0, 80);
        BattleUnit currentUnit6 = new BattleUnit(2, 100);
        BattleUnit targetUnit6 = new BattleUnit(0, 70);

        battleDatas.Add(new BattleData(currentUnit1, targetUnit1, "YOUR_PET"));
        battleDatas.Add(new BattleData(currentUnit2, targetUnit2, "OPPONENT_PET"));
        battleDatas.Add(new BattleData(currentUnit3, targetUnit3, "OPPONENT_PET"));
        battleDatas.Add(new BattleData(currentUnit4, targetUnit4, "YOUR_PET"));
        battleDatas.Add(new BattleData(currentUnit5, targetUnit5, "OPPONENT_PET"));
        battleDatas.Add(new BattleData(currentUnit6, targetUnit6, "OPPONENT_PET"));

        onReceiveBattleDatas(battleDatas, turnBaseController.CurrentPlayer);
        var currentPlayer = players[turnBaseController.CurrentPlayer];
        for (var i = 0; i < battleDatas.Count; i++)
        {
            turnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.FIGHT));
        }
    }
}
