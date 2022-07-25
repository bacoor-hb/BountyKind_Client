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
    public OnEventTriggered OnSetFormationFinished;
    public OnEventTriggered OnBackButtonTrigger;

    private APIManager apiManager;
    [SerializeField]
    private UserCharacter[] userCharacters;
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
    public FormationViewManager viewManager;
    [SerializeField]
    private int selectedAvatarIndex = -1;
    private UserDataManager userDataManager;
    [SerializeField]
    private List<GameObject> avatars;
    private bool canRemove;
    public EventTrigger myEventTrigger;
    public void Init()
    {
        selectedAvatarIndex = -1;
        canRemove = false;
        selectedCharacter = new UserCharacter();
        avatars = new List<GameObject>();
        userDataManager = GlobalManager.Instance.UserDataManager;

        //Set API Event Return
        apiManager = GlobalManager.Instance.NetworkManager.APIManager;
        apiManager.OnGetUserCharactersFinished = null;
        apiManager.OnGetUserCharactersFinished += HandleGetUserCharactersFinished;
        apiManager.OnGetFormationFinished = null;
        apiManager.OnGetFormationFinished += HandleGetFormationFinished;
        apiManager.OnSetFormationFinished = null;
        apiManager.OnSetFormationFinished += HandleSetFormationFinished;
        OnSetFormationFinished = null;

        viewManager.buttonViewManager.SetFormationButton.onClick.AddListener(HandleSetFormation);
        viewManager.buttonViewManager.ResetFormationButton.onClick.AddListener(ResetFormation);
        viewManager.buttonViewManager.RemoveCharacterButton.onClick.AddListener(RemoveCharacter);
        viewManager.buttonViewManager.BackButton.onClick.AddListener(ResetFormationScene);
        viewManager.buttonViewManager.RotateCamRightButton.onClick.AddListener(() => HandleRotateCamera(true));
        viewManager.buttonViewManager.RotateCamLeftButton.onClick.AddListener(() => HandleRotateCamera(false));
        ScrollViewManager.OnInstantiate += HandleOnInstantiate;
        OnAvatarSelected = null;
        OnAvatarSelected += HandleAvatarSelected;
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
            //Debug.Log("HandleGetUserCharactersFinished: " + index);
            avatarPrefab.GetComponent<AvatarController>().SetUserCharacter(userCharacters[index]);
            viewManager.scrollViewManager.RenderAvatar(avatarPrefab, index, userCharacters[index]._id);
        }
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

    /// <summary>
    /// Update Formation Cache and Instantiate Character Prefab
    /// </summary>
    /// <param name="_formationCharacters"></param>
    void HandleGetFormationFinished(FormationCharacters[] _formationCharacters)
    {
        userDataManager.UserGameStatus.UpdateFormationData(_formationCharacters);

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
        selectedCharacter = new UserCharacter();
        selectedSquare = null;
        selectedAvatarIndex = -1;
        OnSelectedSquare?.Invoke(-1);
        OnAvatarSelected?.Invoke(-1);

        if (string.IsNullOrEmpty(result))
        {
            StartCoroutine(viewManager.SetPopupViewState(FormationViewState.SET_FORMATION_FAILED));
        }
        else
        {
            StartCoroutine(viewManager.SetPopupViewState(FormationViewState.SET_FORMATION_SUCCESS));
            OnSetFormationFinished?.Invoke();
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
            OnAvatarSelected?.Invoke(index);
        }
        else
        {
            selectedAvatarIndex = -1;
            selectedCharacter = new UserCharacter();
            selectedSquare = null;
            OnAvatarSelected?.Invoke(-1);
            OnSelectedSquare?.Invoke(-1);
            canRemove = false;
        }
    }

    void SetEventToSquare()
    {
        List<GameObject> boardSquares = viewManager.boardViewManager.boardSquares;
        for (int i = 0; i < boardSquares.Count; i++)
        {
            Debug.Log("SetEventToSquare: " + i);
            boardSquares[i].GetComponent<SquareController>().OnMouseDownEvent += OnClickSquare;
        }
    }

    public void PointerDown_Test(int index)
    {
        Debug.Log("Pointer down: " + index);
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

    void OnClickSquare(int position)
    {
        Debug.Log("OnClickSquare");
        GameObject boardSquare = viewManager.boardViewManager.boardSquares[position];
        selectedSquare = boardSquare;
        if (selectedCharacter.baseKey != null)
        {
            GameObject characterPrefab = GetCharacterPrefabByName(selectedCharacter.baseKey);
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
                    OnAvatarSelected?.Invoke(-1);
                    OnSelectedSquare?.Invoke(-1);
                    canRemove = false;
                }
            }
        }
        else
        {
            Debug.Log("372----:" + avatars.Count);
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

    public void ResetFormationScene()
    {
        OnAvatarSelected(-1);
        OnSelectedSquare(-1);
        viewManager.boardViewManager.DestroyAllCharactersObjs();
        characterWithPositions = new List<CharacterWithPosition>();
        selectedAvatarIndex = -1;
        selectedCharacter = new UserCharacter();
        selectedSquare = null;
        userCharacters = null;
        avatars.ForEach(gameObj =>
        {
            Destroy(gameObj);
        });
        avatars = new List<GameObject>();
        OnBackButtonTrigger?.Invoke();
    }

    void HandleRotateCamera(bool isClockWise)
    {
        viewManager.boardViewManager.RotateCamera(isClockWise);
    }

    void HandleAvatarSelected(int index)
    {
        if (index != -1)
        {
            for (int i = 0; i < avatars.Count; i++)
            {
                if (i != selectedAvatarIndex)
                {
                    avatars[i].GetComponent<AvatarViewManager>().Toggle(false);
                }
                else
                {
                    avatars[i].GetComponent<AvatarViewManager>().Toggle(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < avatars.Count; i++)
            {
                avatars[i].GetComponent<AvatarViewManager>().Toggle(false);
            }
        }
    }

    private void OnDestroy()
    {
        Debug.Log("DestroyedFormationController");
        for (int i = 0; i < viewManager.boardViewManager.boardSquares.Count; i++)
        {
            viewManager.boardViewManager.boardSquares[i].GetComponent<SquareController>().OnMouseDownEvent = null;
        }
        viewManager.boardViewManager.boardSquares = new List<GameObject>();
        OnAvatarSelected = null;
        ScrollViewManager.OnInstantiate -= HandleOnInstantiate;
        myEventTrigger.triggers.RemoveRange(0, myEventTrigger.triggers.Count);
    }
}

public enum FORMATION_SCENE
{
    GAME,
    LOBBY
}
