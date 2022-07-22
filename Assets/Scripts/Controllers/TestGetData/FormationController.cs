using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FormationController : LocalSingleton<FormationController>
{
    public delegate void OnEventTriggered<T>(T data);
    public delegate void OnEventTriggered();
    public OnEventTriggered<int> OnAvatarSelected;
    public OnEventTriggered<string> OnFormationCharatecterReceived;
    public OnEventTriggered<int> OnSelectedSquare;
    public OnEventTriggered OnBackLobbyView;
    // Start is called before the first frame update
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
    public void Init()
    {
        selectedAvatarIndex = -1;
        canRemove = false;
        selectedCharacter = new UserCharacter();
        avatars = new List<GameObject>();
        userDataManager = GlobalManager.Instance.UserDataManager;
        apiManager = GlobalManager.Instance.NetworkManager.APIManager;
        apiManager.OnGetUserCharactersFinished = null;
        apiManager.OnGetUserCharactersFinished += HandleGetUserCharactersFinished;
        apiManager.OnGetFormationFinished = null;
        apiManager.OnGetFormationFinished += HandleGetFormationFinished;
        apiManager.OnGetUserItemsFinished = null;
        apiManager.OnGetUserItemsFinished += HandleGetUserItemsFinished;
        apiManager.OnSetFormationFinished = null;
        apiManager.OnSetFormationFinished += HandleSetFormationFinished;
        viewManager.buttonViewManager.SetFormationButton.onClick.AddListener(HandleSetFormation);
        viewManager.buttonViewManager.ResetFormationButton.onClick.AddListener(ResetFormation);
        viewManager.buttonViewManager.RemoveCharacterButton.onClick.AddListener(RemoveCharacter);
        viewManager.buttonViewManager.BackButton.onClick.AddListener(HandleGoBack);
        viewManager.buttonViewManager.RotateCamRightButton.onClick.AddListener(() => HandleRotateCamera(true));
        viewManager.buttonViewManager.RotateCamLeftButton.onClick.AddListener(() => HandleRotateCamera(false));
        ScrollViewManager.OnInstantiate += HandleOnInstantiate;
        SetEventToSquare();
    }

    /// <summary>
    /// Get user formation
    /// </summary>
    public void GetUserFormationData()
    {
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
        string uri = CONSTS.HOST_ENDPOINT_API + "api/user-characters";
        string address = userDataManager.UserData.address;
        StartCoroutine(apiManager.GetUserCharacters(uri, address));
    }
    void GetUserItems()
    {
        string uri = CONSTS.HOST_ENDPOINT_API + "api/user-items";
        string address = userDataManager.UserData.address;
        StartCoroutine(apiManager.GetUserItems(uri, address));
    }
    void GetUserFormation()
    {
        string uri = CONSTS.HOST_ENDPOINT_API + "api/users/formation";
        string token = userDataManager.UserData.token;
        StartCoroutine(apiManager.GetFormation(uri, token));
    }

    void HandleGetUserCharactersFinished(UserCharacters_API _userCharacters)
    {
        userCharacters = _userCharacters.data;
        for (int i = 0; i < userCharacters.Length; i++)
        {
            int index = i;
            Debug.Log("HandleGetUserCharactersFinished: " + index);
            avatarPrefab.GetComponent<AvatarController>().SetUserCharacter(userCharacters[index]);
            viewManager.scrollViewManager.RenderAvatar(avatarPrefab, index, userCharacters[index]._id);
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
        string uri = CONSTS.HOST_ENDPOINT_API + "api/users/formation";
        string token = userDataManager.UserData.token;
        StartCoroutine(apiManager.SetFormation(uri, token, characterWithPositions));
    }

    void HandleSetFormationFinished(string result)
    {
        if (result != null)
        {
            StartCoroutine(viewManager.SetPopupViewState(1));
        }
        else
        {
            StartCoroutine(viewManager.SetPopupViewState(0));
        }
    }

    void HandleOnInstantiate(GameObject obj, int index)
    {
        Debug.Log("HandleOnInstantiate: " + index);
        avatars.Add(obj);
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) =>
        {
            Debug.Log("HandleOnInstantiate callback: " + index);
            OnClickAvatar(obj.GetComponent<AvatarController>().GetUserCharacter(), index);
        });
        trigger.triggers.Add(entry);
    }

    void OnClickAvatar(UserCharacter userCharacter, int index)
    {
        Debug.Log("OnClickAvatar: " + index);
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
                if (selectedCharacter._id != characterWithPositions[existedPositionIndex].characterId)
                {
                    viewManager.boardViewManager.Clear(characterWithPositions[existedPositionIndex].characterId);
                    characterWithPositions.RemoveAt(existedPositionIndex);
                    viewManager.boardViewManager.RenderCharacterModelToSquare(characterPrefab, position, selectedCharacter._id);
                    characterWithPositions.Add(new CharacterWithPosition(selectedCharacter._id, position));
                    OnSelectedSquare?.Invoke(boardSquare.GetComponent<SquareController>().position);
                    canRemove = false;
                    selectedCharacter = new UserCharacter();
                    selectedSquare = null;
                    OnSelectedSquare?.Invoke(-1);
                    OnAvatarSelected?.Invoke(-1);
                    selectedAvatarIndex = -1;
                }
                else
                {
                    characterWithPositions[existedPositionIndex].position = position;
                    viewManager.boardViewManager.Move(selectedCharacter._id, selectedSquare.transform);
                    OnSelectedSquare?.Invoke(boardSquare.GetComponent<SquareController>().position);
                }
            }
            if (result.Length > 0 && existedPositionIndex == -1)
            {
                characterWithPositions[result[0]].position = position;
                viewManager.boardViewManager.Move(selectedCharacter._id, selectedSquare.transform);
                OnSelectedSquare?.Invoke(boardSquare.GetComponent<SquareController>().position);
            }
            if (existedPositionIndex == -1 && result.Length == 0)
            {
                if (characterWithPositions.Count < 3)
                {
                    viewManager.boardViewManager.RenderCharacterModelToSquare(characterPrefab, position, selectedCharacter._id);
                    characterWithPositions.Add(new CharacterWithPosition(selectedCharacter._id, position));
                    OnSelectedSquare?.Invoke(boardSquare.GetComponent<SquareController>().position);
                }
                else
                {
                    canRemove = false;
                    selectedCharacter = new UserCharacter();
                    selectedSquare = null;
                    OnSelectedSquare?.Invoke(-1);
                    OnAvatarSelected?.Invoke(-1);
                    selectedAvatarIndex = -1;
                }
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
        OnBackLobbyView?.Invoke();
    }

    void HandleRotateCamera(bool isClockWise)
    {
        viewManager.boardViewManager.RotateCamera(isClockWise);
    }

    private void OnDestroy()
    {
        Debug.Log("Destroyed");
    }
}
