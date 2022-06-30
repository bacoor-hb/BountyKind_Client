using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDataController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private APIManager apiManager;
    [SerializeField]
    private UserCharacter[] userCharacters;
    [SerializeField]
    private FormationCharacters[] formationCharacters;
    [SerializeField]
    private UserItem[] userItems;
    [SerializeField]
    private GameObject avatarPrefab;
    [SerializeField]
    private GameObject charactersHolder;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject panel;
    void Start()
    {
        apiManager.OnGetUserCharactersFinished += HandleGetUserCharactersFinished;
        apiManager.OnGetFormationFinished += HandleGetFormationFinished;
        apiManager.OnGetUserItemsFinished += HandleGetUserItemsFinished;
        avatarPrefab.GetComponent<DragDrop>().canvas = canvas;
        avatarPrefab.GetComponent<DragDrop>().panelParent = panel;
        GetUserCharacters();
        GetUserFormation();
        GetUserItems();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetUserCharacters()
    {
        string uri = "https://dev-game-api.w3w.app/api/user-characters";
        string address = "0x64470E5F5DD38e497194BbcAF8Daa7CA578926F6";
        StartCoroutine(apiManager.GetUserCharacters(uri, address));
    }
    void GetUserItems()
    {
        string uri = "https://dev-game-api.w3w.app/api/user-items";
        string address = "0x64470E5F5DD38e497194BbcAF8Daa7CA578926F6";
        StartCoroutine(apiManager.GetUserItems(uri, address));
    }
    void GetUserFormation()
    {
        string uri = "https://dev-game-api.w3w.app/api/users/formation";
        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZGRyZXNzIjoiMHg2NDQ3MGU1ZjVkZDM4ZTQ5NzE5NGJiY2FmOGRhYTdjYTU3ODkyNmY2IiwidXNlcm5hbWUiOiJDw7RuZyBDRU8iLCJpbWFnZSI6IlFtYXBTZkRFeVRSUDNndDhtN3dSR2tBWHJFYTd5SlFmcjZvYVl6b2kyemFvTWIiLCJpc1JlY2VpdmVkIjp0cnVlLCJpc05ld1VzZXIiOmZhbHNlLCJpYXQiOjE2NTY0NzE5MDksImV4cCI6MTk3MjA0NzkwOX0.JFSZqDY-hbOKwlkD0n2NN6PTmyA2gqiEdXvsMrO9g5Q";
        StartCoroutine(apiManager.GetFormation(uri, token));
    }

    void HandleGetUserCharactersFinished(UserCharacters_API _userCharacters)
    {
        userCharacters = _userCharacters.data;
        for (var i = 0; i < userCharacters.Length; i++)
        {
            Instantiate(avatarPrefab, new Vector3(0, 0, 0), Quaternion.identity, charactersHolder.transform);
        }
    }

    void HandleGetUserItemsFinished(UserItems_API _userItems)
    {
        userItems = _userItems.data;
    }

    void HandleGetFormationFinished(FormationCharacters[] _formationCharacters)
    {
        formationCharacters = _formationCharacters;
    }
}
