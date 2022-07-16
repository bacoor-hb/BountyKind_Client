using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationViewManager : MonoBehaviour
{
    // Start is called before the first frame update\
    public ButtonViewManager buttonViewManager;
    public ScrollViewManager scrollViewManager;
    public BoardViewManager boardViewManager;
    public void Init()
    {
        buttonViewManager.Init();
        scrollViewManager.Init();
        boardViewManager.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
