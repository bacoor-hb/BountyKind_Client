using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovementController : MonoBehaviour
{
    [SerializeField] private MovementController movementController;

    [SerializeField] private Transform[] targetsList;
    [SerializeField] private float timeToTarget;
    int currentTarget;

    private void Start()
    {
        movementController.OnStartMoving += LogStart;
        movementController.OnEndMoving += LogEnd;

        currentTarget = 0;
        MoveTheObject();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    movementController.SetTarget(targetsList[currentTarget].position, timeToTarget);
        //    currentTarget = (currentTarget + 1) % (targetsList.Length - 1);
        //    movementController.StartMoving();
        //}
    }

    private void LogStart ()
    {
        Debug.Log("Start moving");
    }

    private void LogEnd()
    {
        Debug.Log("End moving");

        MoveTheObject();
    }

    void MoveTheObject()
    {
        movementController.SetTarget(targetsList[currentTarget].position, timeToTarget);
        currentTarget = (currentTarget + 1) % (targetsList.Length - 1);
        movementController.StartMoving();
    }
}
