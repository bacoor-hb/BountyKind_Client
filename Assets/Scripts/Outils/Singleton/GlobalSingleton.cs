using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSingleton<T> : LocalSingleton<T> where T : Component
{
    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            DontDestroyOnLoad(gameObject);
        }        
    }
}
