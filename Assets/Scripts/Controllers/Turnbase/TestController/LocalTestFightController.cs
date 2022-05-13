using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleUnit
{
    public int id;
    public int health;
    public BattleUnit(int _id, int _health) {
        id = _id;
        health = _health;
    }
}
public class BattleData
{
    public BattleUnit currentUnit;
    public BattleUnit targetUnit;
    public BattleData(BattleUnit _currentUnit, BattleUnit _targetUnit)
    {
        currentUnit = _currentUnit;
        targetUnit = _targetUnit;
    }
}
public class LocalTestFightController : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

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

    // Start is called before the first frame update
    void Start()
    {
        players = new List<PlayerTestFightController>();
        for(var i = 0; i < 3; i++)
        {
            Vector3 pos = spawnPosPets[i].GetComponent<Transform>().position;
            GameObject playerObj = Instantiate(prefab, pos, Quaternion.identity);
            players.Add(playerObj.GetComponent<PlayerTestFightController>());         
        }
        for(var i = 0; i < 3; i++)
        {
            Vector3 pos = spawnPosEnemies[i].GetComponent<Transform>().position;
            GameObject playerObj = Instantiate(prefab, pos, Quaternion.identity);
            players.Add(playerObj.GetComponent<PlayerTestFightController>());
        }
        turnBaseController.Init();
        //Set Player id ~ The turn order
        for (int i = 0; i < players.Count; i++)
        {
            players[i].InitPlayer(i, turnBaseController);
            turnBaseController.Register(players[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        var currentPlayer = players[turnBaseController.CurrentPlayer];
        if (Input.GetKeyDown(KeyCode.A))
        {
            turnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.FIGHT));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("End turnnn");
            turnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.END_TURN));
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
}
