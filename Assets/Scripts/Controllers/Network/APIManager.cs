using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public delegate void OnEventFinished<T>(T returnData);
    public OnEventFinished<UserData_API> OnGetUserDataFinished;
    public OnEventFinished<UserCharacters_API> OnGetUserCharactersFinished;
    public OnEventFinished<FormationCharacters[]> OnGetFormationFinished;
    public OnEventFinished<string> OnSetFormationFinished;
    public OnEventFinished<UserItems_API> OnGetUserItemsFinished;

    public void Init()
    {
        OnGetUserDataFinished = null;
    }

    #region Get API
    /// <summary>
    /// Get Detail Room from the Server
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="map_Key"></param>
    /// <returns></returns>
    //public IEnumerator GetMapDetail(string uri, string map_Key)
    //{
    //    Debug.Log("[APIManager][GetMapDetail] URI: " + uri);
    //    using (UnityWebRequest webRequest = UnityWebRequest.Get(uri + "/" + map_Key))
    //    {
    //        yield return webRequest.SendWebRequest();
    //        string[] pages = uri.Split('/');
    //        int page = pages.Length - 1;
    //        switch (webRequest.result)
    //        {
    //            case UnityWebRequest.Result.ConnectionError:
    //            case UnityWebRequest.Result.DataProcessingError:
    //                Debug.LogError("[APIManager]" + pages[page] + ": GetMapDetail Error: " + webRequest.error);
    //                OnGetAllMapFinished?.Invoke(null);
    //                break;
    //            case UnityWebRequest.Result.ProtocolError:
    //                Debug.LogError("[APIManager]" + pages[page] + ": GetMapDetail HTTP Error: " + webRequest.error);
    //                OnGetAllMapFinished?.Invoke(null);
    //                break;
    //            case UnityWebRequest.Result.Success:
    //                string responseData = webRequest.downloadHandler.text;
    //                GetMapsDetailResponse getMapsResponse = JsonUtility.FromJson<GetMapsDetailResponse>(responseData);
    //                Debug.Log("[APIManager] GetMapDetail Success...");
    //                OnGetMapDetailFinished?.Invoke(getMapsResponse.data);
    //                OnGetMapDetailFinished = null;
    //                break;
    //        }
    //    }
    //}

    /// <summary>
    /// Call Get <see cref="UserData_API"/> API.
    /// </summary>
    /// <param name="uri">The Endpoint</param>
    /// <param name="_token">The token get from Login</param>
    /// <returns></returns>
    public IEnumerator GetUserData(string uri, string _token)
    {
        Debug.Log("[APIManager][GetUserData] URI: " + (uri));
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            string Bearer = "Bearer " + _token;
            webRequest.SetRequestHeader("Authorization", Bearer);
            yield return webRequest.SendWebRequest();
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("[APIManager] " + pages[page] + ": GetUserData Error: " + webRequest.error);
                    OnGetUserDataFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("[APIManager] " + pages[page] + ": GetUserData HTTP Error: " + webRequest.error);
                    OnGetUserDataFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.Success:
                    string responseData = webRequest.downloadHandler.text;
                    UserData_MSG getUserData = JsonUtility.FromJson<UserData_MSG>(responseData);
                    Debug.Log("[APIManager] GetUserData Success: " + responseData);
                    OnGetUserDataFinished?.Invoke(getUserData.data);
                    OnGetUserDataFinished = null;
                    break;
            }
        }
    }
    public IEnumerator GetUserCharacters(string uri, string address)
    {
        Debug.Log("[APIManager][GetUserCharacters] URI: " + (uri));
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri + "?owner=" + address))
        {
            yield return webRequest.SendWebRequest();
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("[APIManager] " + pages[page] + ": GetUserCharacters Error: " + webRequest.error);
                    OnGetUserCharactersFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("[APIManager] " + pages[page] + ": GetUserCharacters HTTP Error: " + webRequest.error);
                    OnGetUserCharactersFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.Success:
                    string responseData = webRequest.downloadHandler.text;
                    UserCharactersResponse getUserCharactersData = JsonUtility.FromJson<UserCharactersResponse>(responseData);
                    Debug.Log("[APIManager] GetUserCharacters Success: " + responseData);
                    OnGetUserCharactersFinished?.Invoke(getUserCharactersData.data);
                    OnGetUserCharactersFinished = null;
                    break;
            }
        }
    }
    public IEnumerator GetUserItems(string uri, string address)
    {
        Debug.Log("[APIManager][GetUserItems] URI: " + (uri));
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri + "?owner=" + address))
        {
            yield return webRequest.SendWebRequest();
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("[APIManager] " + pages[page] + ": GetUserItems Error: " + webRequest.error);
                    OnGetUserItemsFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("[APIManager] " + pages[page] + ": GetUserItems HTTP Error: " + webRequest.error);
                    OnGetUserItemsFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.Success:
                    string responseData = webRequest.downloadHandler.text;
                    UserItemsResponse getUserItems = JsonUtility.FromJson<UserItemsResponse>(responseData);
                    Debug.Log("[APIManager] GetUserItems Success: " + responseData);
                    OnGetUserItemsFinished?.Invoke(getUserItems.data);
                    OnGetUserItemsFinished = null;
                    break;
            }
        }
    }
    public IEnumerator GetFormation(string uri, string _token)
    {
        Debug.Log("[APIManager][GetFormation] URI: " + (uri));
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            string Bearer = "Bearer " + _token;
            webRequest.SetRequestHeader("Authorization", Bearer);
            yield return webRequest.SendWebRequest();
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("[APIManager] " + pages[page] + ": GetFormation Error: " + webRequest.error);
                    OnGetFormationFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("[APIManager] " + pages[page] + ": GetFormation HTTP Error: " + webRequest.error);
                    OnGetFormationFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.Success:
                    string responseData = webRequest.downloadHandler.text;
                    UserFormationResponse getUserFormation = JsonUtility.FromJson<UserFormationResponse>(responseData);
                    Debug.Log("[APIManager] GetFormation Success: " + responseData);
                    OnGetFormationFinished?.Invoke(getUserFormation.data);
                    OnGetFormationFinished = null;
                    break;
            }
        }
    }
    #endregion
    #region Put API
    public IEnumerator SetFormation(string uri, string _token, List<CharacterWithPosition> characterWithPositions)
    {
        Debug.Log("[APIManager][SetFormation] URI: " + (uri));
        string json = JsonConvert.SerializeObject(characterWithPositions);
        using (UnityWebRequest webRequest = UnityWebRequest.Put(uri, json))
        {
            string Bearer = "Bearer " + _token;
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", Bearer);
            yield return webRequest.SendWebRequest();
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("[APIManager] " + pages[page] + ": SetFormation Error: " + webRequest.error);
                    OnSetFormationFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("[APIManager] " + pages[page] + ": SetFormation HTTP Error: " + webRequest.error);
                    OnSetFormationFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.Success:
                    string responseData = webRequest.downloadHandler.text;
                    SetFormationResponse setUserFormation = JsonUtility.FromJson<SetFormationResponse>(responseData);
                    Debug.Log("[APIManager] SetFormation Success: " + responseData);
                    OnSetFormationFinished?.Invoke(setUserFormation.message);
                    OnSetFormationFinished = null;
                    break;
            }
        }
    }
    #endregion
}
