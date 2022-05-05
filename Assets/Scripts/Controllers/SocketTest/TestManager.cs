 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{
    [SerializeField]
    private Transform content;
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
        bountyColyseusManager.JoinLobby();
        AddListeners();
    }   

    void AddListeners()
    {
        JoinRoomBTN.onClick.AddListener(() => { HandleJoinRoom(); });
        RollButton.onClick.AddListener(() => { HandleRoll(); });
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
