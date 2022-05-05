using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameController : MonoBehaviour
{
    [SerializeField]
    private List<PlayerTestController> players;

    [SerializeField]
    private TurnBaseController turnBaseController;

    // Start is called before the first frame update
    void Start()
    {
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
       
    }
}
