using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class UserController : MonoBehaviour
{
    private UserDataManager UserDataManager;
    private NetworkManager NetworkManager;
    private LoadingManager LoadingManager;

    void Start()
    {
        UserDataManager = GlobalManager.Instance.UserDataManager;
        NetworkManager = GlobalManager.Instance.NetworkManager;
        LoadingManager = GlobalManager.Instance.LoadingManager;

       
    }
}
