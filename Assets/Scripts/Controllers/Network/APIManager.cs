using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public delegate void OnEventFinished<T>(T returnData);
    public OnEventFinished<List<BountyMap_Short>> OnGetAllMapFinished;
    public OnEventFinished<BountyMap> OnGetMapDetailFinished;
    public OnEventFinished<UserData_API> OnGetUserDataFinished;

    public void Init()
    {
        OnGetAllMapFinished = null;
        OnGetUserDataFinished = null;
    }

    #region Get API
    /// <summary>
    /// Get All Room Type from the Server
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    public IEnumerator GetAllMapTypes(string uri)
    {
        Debug.Log("[APIManager][GetAllMapTypes] URI: " + uri);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("[APIManager]" + pages[page] + ": GetAllMapTypes Error: " + webRequest.error);
                    OnGetAllMapFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("[APIManager]" + pages[page] + ": GetAllMapTypes HTTP Error: " + webRequest.error);
                    OnGetAllMapFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.Success:
                    string responseData = webRequest.downloadHandler.text;
                    GetMapsResponse_Short getMapsResponse = JsonUtility.FromJson<GetMapsResponse_Short>(responseData);
                    Debug.Log("[APIManager] GetAllMapTypes Success...");
                    OnGetAllMapFinished?.Invoke(getMapsResponse.data);
                    OnGetAllMapFinished = null;
                    break;
            }
        }
    }

    /// <summary>
    /// Get Detail Room from the Server
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="map_Key"></param>
    /// <returns></returns>
    public IEnumerator GetMapDetail(string uri, string map_Key)
    {
        Debug.Log("[APIManager][GetMapDetail] URI: " + uri);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri + "/" + map_Key))
        {
            yield return webRequest.SendWebRequest();
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("[APIManager]" + pages[page] + ": GetMapDetail Error: " + webRequest.error);
                    OnGetAllMapFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("[APIManager]" + pages[page] + ": GetMapDetail HTTP Error: " + webRequest.error);
                    OnGetAllMapFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.Success:
                    string responseData = webRequest.downloadHandler.text;
                    GetMapsDetailResponse getMapsResponse = JsonUtility.FromJson<GetMapsDetailResponse>(responseData);
                    Debug.Log("[APIManager] GetMapDetail Success...");
                    OnGetMapDetailFinished?.Invoke(getMapsResponse.data);
                    OnGetMapDetailFinished = null;
                    break;
            }
        }
    }

    /// <summary>
    /// Call Get <see cref="UserData_API"/> API.
    /// </summary>
    /// <param name="uri">The Endpoint</param>
    /// <param name="_token">The token get from Login</param>
    /// <returns></returns>
    public IEnumerator GetUserData(string uri, string _address, string _token)
    {
        Debug.Log("[APIManager][GetUserData] URI: " + (uri + _address));
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri + _address))
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
                    Debug.Log("[APIManager] GetUserData Success: "+ responseData);
                    OnGetUserDataFinished?.Invoke(getUserData.data);
                    OnGetUserDataFinished = null;
                    break;
            }
        }
    }
    #endregion
}
