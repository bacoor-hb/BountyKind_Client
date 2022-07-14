using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FormationController : MonoBehaviour
{
    public delegate void OnEventTriggered<T>(T data);
    public static event OnEventTriggered<int> OnAvatarSelected;
    public static event OnEventTriggered<string> OnFormationCharatecterReceived;
    public static event OnEventTriggered<int> OnSelectedSquare;
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
    private UserCharacter selectedCharacter;
    [SerializeField]
    private GameObject selectedSquare;
    [SerializeField]
    private List<CharacterWithPosition> characterWithPositions;
    [SerializeField]
    private FormationViewManager viewManager;
    [SerializeField]
    private int selectedAvatarIndex = -1;
    private UserDataManager userDataManager;
    [SerializeField]
    private List<GameObject> avatars;
    private bool canRemove;
    void Start()
    {
        canRemove = false;
        selectedCharacter = new UserCharacter();
        avatars = new List<GameObject>();
        userDataManager = GlobalManager.Instance.UserDataManager;
        apiManager.OnGetUserCharactersFinished += HandleGetUserCharactersFinished;
        apiManager.OnGetFormationFinished += HandleGetFormationFinished;
        apiManager.OnGetUserItemsFinished += HandleGetUserItemsFinished;
        viewManager.buttonViewManager.SetFormationButton.onClick.AddListener(HandleSetFormation);
        viewManager.buttonViewManager.ResetFormationButton.onClick.AddListener(ResetFormation);
        viewManager.buttonViewManager.RemoveCharacterButton.onClick.AddListener(RemoveCharacter);
        viewManager.buttonViewManager.BackButton.onClick.AddListener(HandleGoBack);
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

    void RemoveCharacter()
    {
        if (canRemove)
        {
            int index = characterWithPositions.FindIndex(characterWithPosition => characterWithPosition.characterId == selectedCharacter._id);
            characterWithPositions.RemoveAt(index);
            viewManager.boardViewManager.Clear(selectedCharacter._id);
            selectedCharacter = new UserCharacter();
            selectedSquare = null;
            OnSelectedSquare?.Invoke(-1);
            OnAvatarSelected?.Invoke(-1);
            selectedAvatarIndex = -1;
            canRemove = false;
        }
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
        avatars.Add(obj);
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
            selectedSquare = viewManager.boardViewManager.boardSquares[result[1]];
            OnSelectedSquare?.Invoke(result[1]);
            canRemove = true;
        }
        else
        {
            selectedSquare = null;
            OnSelectedSquare?.Invoke(-1);
            canRemove = false;
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
            selectedCharacter = new UserCharacter();
            selectedSquare = null;
            OnAvatarSelected(-1);
            OnSelectedSquare(-1);
            canRemove = false;
        }
    }

    void SetEventToSquare()
    {
        Debug.Log("Set events to square");
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
        if (selectedCharacter.baseKey != null)
        {
            GameObject characterPrefab = GetCharacterPrefabByName(selectedCharacter.baseKey);
            int position = selectedSquare.GetComponent<SquareController>().position;
            int existedPositionIndex = CheckIfPositionExisted(position);
            int[] result = CheckIfCharacterExisted(selectedCharacter);
            if (existedPositionIndex != -1 && result.Length == 0)
            {
                characterWithPositions[existedPositionIndex].position = position;
                viewManager.boardViewManager.Move(selectedCharacter._id, selectedSquare.transform);
                OnSelectedSquare?.Invoke(boardSquare.GetComponent<SquareController>().position);
            }
            if (result.Length > 0 && existedPositionIndex == -1)
            {
                characterWithPositions[result[0]].position = position;
                viewManager.boardViewManager.Move(selectedCharacter._id, selectedSquare.transform);
                OnSelectedSquare?.Invoke(boardSquare.GetComponent<SquareController>().position);
            }
            if (existedPositionIndex == -1 && result.Length == 0)
            {
                viewManager.boardViewManager.RenderCharacterModelToSquare(characterPrefab, position, selectedCharacter._id);
                characterWithPositions.Add(new CharacterWithPosition(selectedCharacter._id, position));
                OnSelectedSquare?.Invoke(boardSquare.GetComponent<SquareController>().position);
            }
            if (existedPositionIndex != -1 && result.Length > 0)
            {
                if (position != result[1])
                {
                    Transform tempTransform = viewManager.boardViewManager.boardSquares[result[1]].transform;
                    int tempPosition = result[1];
                    characterWithPositions[result[0]].position = position;
                    characterWithPositions[existedPositionIndex].position = tempPosition;
                    viewManager.boardViewManager.Move(selectedCharacter._id, selectedSquare.transform);
                    viewManager.boardViewManager.Move(characterWithPositions[existedPositionIndex].characterId, tempTransform);
                    OnSelectedSquare?.Invoke(boardSquare.GetComponent<SquareController>().position);
                }
                else
                {
                    selectedAvatarIndex = -1;
                    selectedCharacter = new UserCharacter();
                    selectedSquare = null;
                    OnAvatarSelected(-1);
                    OnSelectedSquare(-1);
                    canRemove = false;
                }
            }
        }
        else
        {
            int position = selectedSquare.GetComponent<SquareController>().position;
            int existedPositionIndex = CheckIfPositionExisted(position);
            if (existedPositionIndex != -1)
            {
                OnSelectedSquare?.Invoke(boardSquare.GetComponent<SquareController>().position);
                string characterId = characterWithPositions[existedPositionIndex].characterId;
                for (int i = 0; i < avatars.Count; i++)
                {
                    if (avatars[i].GetComponent<AvatarController>().GetUserCharacter()._id == characterId)
                    {
                        selectedAvatarIndex = i;
                        OnAvatarSelected?.Invoke(i);
                        selectedCharacter = avatars[i].GetComponent<AvatarController>().GetUserCharacter();
                        canRemove = true;
                    }
                }
            }
        }
    }

    void HandleGoBack()
    {
        GlobalManager.Instance.LoadingManager.LoadWithLoadingScene(SCENE_NAME.MainMenu);
    }

    private void OnDestroy()
    {
        Debug.Log("Destroyed");
    }
}
