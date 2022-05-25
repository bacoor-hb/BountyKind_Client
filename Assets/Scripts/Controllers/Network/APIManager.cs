using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    public delegate void OnEventFinished<T>(T returnData);
    public OnEventFinished<List<BountyMap>> OnGetMapFinished;
    public OnEventFinished<UserData_API> OnGetUserDataFinished;

    public void Init()
    {
        OnGetMapFinished = null;
        OnGetUserDataFinished = null;
    }

    #region Get API
    /// <summary>
    /// Get All Room Type from the Server and update it on the UI
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
                    OnGetMapFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("[APIManager]" + pages[page] + ": GetAllMapTypes HTTP Error: " + webRequest.error);
                    OnGetMapFinished?.Invoke(null);
                    break;
                case UnityWebRequest.Result.Success:
                    string responseData = webRequest.downloadHandler.text;
                    GetMapsResponse getMapsResponse = JsonUtility.FromJson<GetMapsResponse>(responseData);
                    Debug.Log("[APIManager] GetAllMapTypes Success...");
                    OnGetMapFinished?.Invoke(getMapsResponse.data);
                    OnGetMapFinished = null;
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
