 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestManager : Singleton<TestManager>   
{
    [SerializeField]
    private Transform content;
    [SerializeField]
    private Button JoinLobbyBTN;
    [SerializeField]
    private Button JoinRoomBTN;
    [SerializeField]
    private Button RollButton;
    [SerializeField]
    private GameObject prefabItem;
    private BountyColyseusManager bountyColyseusManager;
    // Start is called before the first frame update
    void Start()
    {
        bountyColyseusManager = this.gameObject.AddComponent<BountyColyseusManager>();
        bountyColyseusManager.Connect();
        AddListeners();
    }   

    void AddListeners()
    {   
        JoinLobbyBTN.onClick.AddListener(() => { HandleJoinLobby(); });
    }
    void HandleJoinLobby()
    {
        bountyColyseusManager.JoinLobby();
        SceneManager.LoadScene("Test_Socket_Lobby");
    }
    void HandleJoinRoom()
    {
        bountyColyseusManager.JoinRoom(ROOM_TYPE.GAME_ROOM);
    }

    void HandleRoll()
    {
        bountyColyseusManager.Roll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    private void OnDestroy()
    {
        bountyColyseusManager.Disconnect();
    }
}
