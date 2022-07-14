using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QueueController : MonoBehaviour
{
    public List<GameObject> unitsInQueue;
    public List<GameObject> unitsOutQueue;

    private void Start()
    {
        unitsInQueue = new List<GameObject>();
        unitsOutQueue = new List<GameObject>();
    }

    private void Update()
    {

    }

    public void SetUnitsInQueue(List<GameObject> _unitsInQueue)
    {
        unitsInQueue = _unitsInQueue;
    }
}
