using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovementController : MonoBehaviour
{
    [SerializeField] private MovementController movementController;

    [SerializeField] private Transform targetTesting;
    [SerializeField] private float timeToTarget;

    private void Start()
    {
        movementController.OnStartMoving += LogStart;
        movementController.OnEndMoving += LogEnd;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movementController.SetTarget(targetTesting.position, this.timeToTarget);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            movementController.StartMoving();
        }
    }

    private void LogStart ()
    {
        Debug.Log("Start moving");
    }

    private void LogEnd()
    {
        Debug.Log("End moving");
    }
}
