using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class LoadingView : MonoBehaviour
{
    [SerializeField]
    private GameObject LoadingCanvas;
    [SerializeField]
    private Slider progressBar;
    [SerializeField]
    private TextMeshProUGUI MsgTxt;

    private float targetProgess = 0;
    public float FillSpeed = 0.0002f;
    void Start()
    {
        
    }

    void Update()
    {
        if(progressBar.value < targetProgess)
        {
            //Debug.Log("Progress :" + progressBar.value);
            progressBar.value += FillSpeed + Time.deltaTime;
        }
    }


    public void Init()
    {

    }

    public void IncrementProgess(float newProgess)
    {
        targetProgess = progressBar.value + newProgess;
        //targetProgess = newProgess;
    }

    public void SetMessage(string _msg)
    {
        MsgTxt.text = _msg;
    }

    public void SetCanvasStatus(bool status)
    {
        if(LoadingCanvas != null)
        {
            LoadingCanvas.SetActive(status);
        }
    }
}
