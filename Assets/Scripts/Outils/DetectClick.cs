using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectClick : MonoBehaviour
{
    [SerializeField]
    private PlayerTestFightController playerTestFightController;
    void Start()
    {
        Debug.Log(playerTestFightController.id);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log(playerTestFightController.id);
    }
}
