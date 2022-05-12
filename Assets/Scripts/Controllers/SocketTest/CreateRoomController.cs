using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class Enemy
{
    public string name { get; set; }
    public int atk { get; set; }
    public int def { get; set; }
    public int hp { get; set; }
    public int position { get; set; }
}
[Serializable]
public class MapNode
{
    public string name;
    public int position;
    public string type;
    public List<Enemy> emeny;
}
[Serializable]
public class BountyMap
{
    public string _id;
    public string key;
    public string name;
    public int totalNode;
    public List<MapNode> nodes;
}
[Serializable]
public class GetMapsResponse
{
    public List<BountyMap> data;
}
public class CreateRoomController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject MapsSelect;
    [SerializeField]
    private Button CreateMapBTN;
    private List<BountyMap> bountyMaps;
    private string selectedMapKey;
    void Start()
    {
        CreateMapBTN.onClick.AddListener(() => { CreateRoom(); });
        StartCoroutine(GetAllRoomTypes("https://dev-game-api.w3w.app/api/maps"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetAllRoomTypes(string uri)
    {
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
                    List<Dropdown.OptionData> m_Messages = new List<Dropdown.OptionData>();
                    for (var i = 0; i < bountyMaps.Count; i++)
                    {
                        Dropdown.OptionData newOption = new Dropdown.OptionData();
                        newOption.text = bountyMaps[i].name;
                        m_Messages.Add(newOption);
                    }
                    foreach (Dropdown.OptionData message in m_Messages)
                    {
                        MapsSelect.GetComponentInChildren<Dropdown>().options.Add(message);
                    }
                    MapsSelect.GetComponentInChildren<Text>().text = m_Messages[0].text;
                    selectedMapKey = bountyMaps[0].key;
                    MapsSelect.GetComponentInChildren<Dropdown>().onValueChanged.AddListener(delegate {
                        selectedMapKey = bountyMaps[MapsSelect.GetComponentInChildren<Dropdown>().value].key;
                    });
                    break;
            }
        } 
    }
    void CreateRoom()
    {
        UserManagement.Instance.CreateRoom(ROOM_TYPE.GAME_ROOM, selectedMapKey);
        SceneManager.LoadScene("Test_Game_Scene");
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy create room controller");
    }
}
