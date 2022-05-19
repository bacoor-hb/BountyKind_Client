using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class RoomEventController : MonoBehaviour
{
    public delegate void OnEventTrigger();
    public OnEventTrigger OnGetMapSuccess;

    [SerializeField]
    private LobbyView LobbyView;

    private List<BountyMap> bountyMaps;
    private string selectedMapKey;

    private NetworkManager NetworkManager;

    public void Init()
    {
        NetworkManager = GlobalManager.Instance.NetworkManager;

        LobbyView.CreateRoom_Btn.enabled = false;
        LobbyView.CreateRoom_Btn.onClick.RemoveAllListeners();
        LobbyView.CreateRoom_Btn.onClick.AddListener(CreateRoom);        
    }

    /// <summary>
    /// Call the Server API to get the room type, then update the dropdown list.
    /// </summary>
    public void GetMap_FromServer()
    {
        Debug.Log("[RoomEventController] GetMap_FromServer: " + CONSTS.HOST_GET_MAP_API);
        StartCoroutine(GetAllMapTypes(CONSTS.HOST_GET_MAP_API));
    }

    /// <summary>
    /// Get All Room Type from the Server and update it on the UI
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    IEnumerator GetAllMapTypes(string uri)
    {
        bountyMaps = new List<BountyMap>();
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    string responseData = webRequest.downloadHandler.text;
                    GetMapsResponse getMapsResponse = JsonUtility.FromJson<GetMapsResponse>(responseData);
                    bountyMaps = getMapsResponse.data;
                    LobbyView.UpdateMapList(bountyMaps);

                    //Set event for the Dropdown List
                    selectedMapKey = bountyMaps[0].key;
                    LobbyView.RoomType_DD.onValueChanged.AddListener((currentMap) => 
                    {
                        selectedMapKey = bountyMaps[currentMap].key;
                    });
                    break;
            }
        }

        if (bountyMaps.Count > 0)
        {
            GetMapSuccess();
        }
    }

    /// <summary>
    /// Trigger this event when the server return the room list.
    /// </summary>
    void GetMapSuccess()
    {
        OnGetMapSuccess?.Invoke();
        LobbyView.CreateRoom_Btn.enabled = true;
    }

    /// <summary>
    /// Trigger the Create room.
    /// </summary>
    void CreateRoom()
    {
        if (bountyMaps != null && bountyMaps.Count > 0)
            NetworkManager.CreateRoom(ROOM_TYPE.GAME_ROOM, selectedMapKey);
        else
            Debug.LogError("[RoomEventController] Create Room ERROR: No Map Data.");
    }
}
