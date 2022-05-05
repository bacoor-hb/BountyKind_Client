using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalLoading : MonoBehaviour
{
    LoadingManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GlobalManager.Instance.LoadingManager;
        SCENE_NAME sceneToLoad = (SCENE_NAME)Enum.Parse(typeof(SCENE_NAME), PlayerPrefs.GetString(CONSTS.SCENE_KEY));
        StartCoroutine(manager.LoadScene_Async(sceneToLoad));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
