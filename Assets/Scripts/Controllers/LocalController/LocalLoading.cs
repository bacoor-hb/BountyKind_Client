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
        SCENE_NAME sceneToLoad = manager.targetScene;
        StartCoroutine(manager.LoadScene_Async(sceneToLoad));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
