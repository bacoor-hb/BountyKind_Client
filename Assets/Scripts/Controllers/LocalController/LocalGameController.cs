using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameController : MonoBehaviour
{
    [SerializeField]
    private List<UserManager> players;

    [SerializeField]
    private MovementController MovementController;
    [SerializeField]
    private DicesController DicesController;
    [SerializeField]
    private TurnBaseController TurnBaseController;

    // Start is called before the first frame update
    void Start()
    {
        
        //Set Player id ~ The turn order
        for (int i = 0; i < players.Count; i++)
        {
            User currentUserData = GetUser(i);
            players[i].Init(currentUserData, TurnBaseController);
            TurnBaseController.Register(players[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    /// <summary>
    /// Get the User Data from the Server/Blockchain
    /// </summary>
    /// <returns></returns>
    User GetUser(int userID)
    {
        User tmp = new User();
        return tmp;
    }
}
