using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FormationController : MonoBehaviour
{
    public delegate void OnEventTriggered<T>(T data);
    public static OnEventTriggered<int> OnAvatarSelected;
    public static OnEventTriggered<string> OnFormationCharatecterReceived;
    public static OnEventTriggered<int> OnSelectedSquare;
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
    private List<GameObject> characterPrefabs;
    [SerializeField]
    private UserCharacter selectedCharacter = null;
    [SerializeField]
    private GameObject selectedSquare;
    [SerializeField]
    private List<CharacterWithPosition> characterWithPositions;
    [SerializeField]
    private FormationViewManager viewManager;
    [SerializeField]
    private int selectedAvatarIndex = -1;
    private UserDataManager userDataManager;
    void Start()
    {
        userDataManager = GlobalManager.Instance.UserDataManager;
        apiManager.OnGetUserCharactersFinished += HandleGetUserCharactersFinished;
        apiManager.OnGetFormationFinished += HandleGetFormationFinished;
        apiManager.OnGetUserItemsFinished += HandleGetUserItemsFinished;
        viewManager.buttonViewManager.SetFormationButton.onClick.AddListener(HandleSetFormation);
        viewManager.buttonViewManager.ResetFormationButton.onClick.AddListener(ResetFormation);
        ScrollViewManager.OnInstantiate += HandleOnInstantiate;
        GetUserCharacters();
        GetUserFormation();
        GetUserItems();
        SetEventToSquare();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GetUserCharacters()
    {
        string uri = "https://dev-game-api.w3w.app/api/user-characters";
        string address = userDataManager.UserData.address;
        StartCoroutine(apiManager.GetUserCharacters(uri, address));
    }
    void GetUserItems()
    {
        string uri = "https://dev-game-api.w3w.app/api/user-items";
        string address = userDataManager.UserData.address;
        StartCoroutine(apiManager.GetUserItems(uri, address));
    }
    void GetUserFormation()
    {
        string uri = "https://dev-game-api.w3w.app/api/users/formation";
        string token = userDataManager.UserData.token;
        StartCoroutine(apiManager.GetFormation(uri, token));
    }

    void HandleGetUserCharactersFinished(UserCharacters_API _userCharacters)
    {
        userCharacters = _userCharacters.data;
        for (var i = 0; i < userCharacters.Length; i++)
        {
            avatarPrefab.GetComponent<AvatarController>().SetUserCharacter(userCharacters[i]);
            viewManager.scrollViewManager.RenderAvatar(avatarPrefab, i, userCharacters[i]._id);
        }
    }

    void HandleGetUserItemsFinished(UserItems_API _userItems)
    {
        userItems = _userItems.data;
    }

    void HandleGetFormationFinished(FormationCharacters[] _formationCharacters)
    {
        formationCharacters = _formationCharacters;
        if (_formationCharacters.Length > 0)
        {
            for (var i = 0; i < _formationCharacters.Length; i++)
            {
                int position = _formationCharacters[i].position;
                string _id = _formationCharacters[i]._id;
                characterWithPositions.Add(new CharacterWithPosition(_id, position));
                GameObject characterPrefab = GetCharacterPrefabByName(_formationCharacters[i].baseKey);
                viewManager.boardViewManager.RenderCharacterModelToSquare(characterPrefab, position, _id);
            }
        }
    }
    GameObject GetCharacterPrefabByName(string name)
    {
        GameObject characterPrefab = null;
        for (var i = 0; i < characterPrefabs.Count; i++)
        {
            if (characterPrefabs[i].name == name)
            {
                characterPrefab = characterPrefabs[i];
            }
        }
        return characterPrefab;
    }

    void ResetFormation()
    {
        if (characterWithPositions.Count > 0)
        {
            Debug.Log("Reset");
            for (var i = 0; i < characterWithPositions.Count; i++)
            {
                viewManager.boardViewManager.Clear(characterWithPositions[i].characterId);
            }
            characterWithPositions = new List<CharacterWithPosition>();
        }
    }
    void HandleSetFormation()
    {
        string uri = "https://dev-game-api.w3w.app/api/users/formation";
        string token = userDataManager.UserData.token;
        StartCoroutine(apiManager.SetFormation(uri, token, characterWithPositions));
    }

    void HandleOnInstantiate(GameObject obj, int index)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) =>
        {
            OnClickAvatar(obj.GetComponent<AvatarController>().GetUserCharacter(), index);
        });
        trigger.triggers.Add(entry);
    }

    void OnClickAvatar(UserCharacter userCharacter, int index)
    {
        int[] result = CheckIfCharacterExisted(userCharacter);
        if (result.Length > 0)
        {
            OnSelectedSquare?.Invoke(result[1]);
        }
        selectedCharacter = userCharacter;
        if (selectedAvatarIndex != index)
        {
            selectedAvatarIndex = index;
            OnAvatarSelected(index);
        }
        else
        {
            selectedAvatarIndex = -1;
            selectedCharacter = null;
            selectedSquare = null;
            OnAvatarSelected(-1);
            OnSelectedSquare(-1);
        }
    }

    void SetEventToSquare()
    {
        List<GameObject> boardSquares = viewManager.boardViewManager.boardSquares;
        for (int i = 0; i < boardSquares.Count; i++)
        {
            EventTrigger trigger = boardSquares[i].GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            int index = i;
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((data) =>
            {
                OnClickSquare((PointerEventData)data, boardSquares[index]);
            });
            trigger.triggers.Add(entry);
        }
    }
    int CheckIfPositionExisted(int position)
    {
        int existedPositionIndex = -1;
        for (int i = 0; i < characterWithPositions.Count; i++)
        {
            if (characterWithPositions[i].position == position)
            {
                existedPositionIndex = i;
            }
        }
        return existedPositionIndex;
    }

    int[] CheckIfCharacterExisted(UserCharacter userCharacter)
    {
        int[] result = new int[] { };
        for (int i = 0; i < characterWithPositions.Count; i++)
        {
            if (characterWithPositions[i].characterId == userCharacter._id)
            {
                int existedCharacterIndex = i;
                int existedCharacterPosition = characterWithPositions[i].position;
                int[] _result = new int[] { existedCharacterIndex, existedCharacterPosition };
                result = _result;
            }
        }
        return result;
    }

    void OnClickSquare(PointerEventData eventData, GameObject boardSquare)
    {
        selectedSquare = boardSquare;
        if (selectedCharacter != null && selectedCharacter.baseKey != "")
        {
            GameObject characterPrefab = GetCharacterPrefabByName(selectedCharacter.baseKey);
            int position = selectedSquare.GetComponent<SquareController>().position;
            int existedPositionIndex = CheckIfPositionExisted(position);
            int[] result = CheckIfCharacterExisted(selectedCharacter);
            if (existedPositionIndex != -1 && result.Length == 0)
            {
                characterWithPositions[existedPositionIndex].position = position;
                viewManager.boardViewManager.Move(selectedCharacter._id, selectedSquare.transform);
            }
            if (result.Length > 0 && existedPositionIndex == -1)
            {
                characterWithPositions[result[0]].position = position;
                viewManager.boardViewManager.Move(selectedCharacter._id, selectedSquare.transform);
            }
            if (existedPositionIndex == -1 && result.Length == 0)
            {
                viewManager.boardViewManager.RenderCharacterModelToSquare(characterPrefab, position, selectedCharacter._id);
                characterWithPositions.Add(new CharacterWithPosition(selectedCharacter._id, position));
            }
            if (existedPositionIndex != -1 && result.Length > 0)
            {
                Transform tempTransform = viewManager.boardViewManager.boardSquares[result[1]].transform;
                int tempPosition = result[1];
                characterWithPositions[result[0]].position = position;
                characterWithPositions[existedPositionIndex].position = tempPosition;
                viewManager.boardViewManager.Move(selectedCharacter._id, selectedSquare.transform);
                viewManager.boardViewManager.Move(characterWithPositions[existedPositionIndex].characterId, tempTransform);
            }
            OnSelectedSquare?.Invoke(boardSquare.GetComponent<SquareController>().position);
        }
        else
        {
            int position = selectedSquare.GetComponent<SquareController>().position;
            int existedPositionIndex = CheckIfPositionExisted(position);
            if (existedPositionIndex != -1)
            {
                OnSelectedSquare?.Invoke(boardSquare.GetComponent<SquareController>().position);
                if (eventData.clickCount == 2)
                {
                    viewManager.boardViewManager.Clear(characterWithPositions[existedPositionIndex].characterId);
                    characterWithPositions.RemoveAt(existedPositionIndex);
                    OnSelectedSquare(-1);
                }
            }
        }
    }
}
