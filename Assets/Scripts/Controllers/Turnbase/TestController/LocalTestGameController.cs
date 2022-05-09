using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTestGameController : MonoBehaviour
{
    [SerializeField]
    private List<PlayerTestController> players;

    [SerializeField]
    private TurnBaseController turnBaseController;

    // Start is called before the first frame update
    void Start()
    {
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
            turnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.MOVE));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            turnBaseController.AddAction(currentPlayer, currentPlayer.GetAction(ACTION_TYPE.ROLL_DICE));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
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
