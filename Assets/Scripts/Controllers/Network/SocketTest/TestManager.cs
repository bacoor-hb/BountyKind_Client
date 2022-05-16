 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestManager : LocalSingleton<TestManager>   
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
    private NetworkManager NetworkManager;

    // Start is called before the first frame update
    void Start()
    {
        NetworkManager = GlobalManager.Instance.NetworkManager;
        AddListeners();
    }   

    void AddListeners()
    {
        JoinRoomBTN.onClick.AddListener(() => { HandleJoinRoom(); });
    }
    void HandleJoinLobby()
    {
        bountyColyseusManager.JoinLobby("asd");
        SceneManager.LoadScene("Test_Socket_Lobby");
    }
    void HandleJoinRoom()
    {
        Debug.Log("join room");
        SceneManager.LoadScene("Test_Socket_GameRoom");
    }

    void HandleRoll()
    {
        NetworkManager.Send(PLAYER_SENT_EVENTS.ROLL_DICE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }   

    private void OnDestroy()
    {
        
    }
}
