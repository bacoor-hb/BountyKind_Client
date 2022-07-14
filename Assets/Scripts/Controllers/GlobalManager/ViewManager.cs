using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : GlobalSingleton<ViewManager>
{
    public GlobalErrorView GlobalErrorView { get; private set; }

    // Start is called before the first frame update
    public void Init()
    {
        GlobalErrorView = GetComponentInChildren<GlobalErrorView>();
        GlobalErrorView.Init();
    }
}
