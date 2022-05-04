using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public delegate void OnEventCalled();
    public OnEventCalled OnStartMoving;
    public OnEventCalled OnEndMoving;

    [Tooltip ("The Object to Move by this Script")]
    [SerializeField] private GameObject ObjectToMove;
    private Vector3 startPos;
    private Vector3 target;

    private float countdownTime;
    [SerializeField]
    private float timeToTarget;
    private bool inTarget = true;
    private bool isMoving = false;

    private void Update()
    {
        if (isMoving)
        {
            countdownTime += Time.deltaTime / timeToTarget;
            ObjectToMove.transform.position = Vector3.Lerp(startPos, target, countdownTime);
            ObjectToMove.transform.LookAt(target);
        }

        if (!inTarget && Vector3.Distance(ObjectToMove.transform.position, target) < 0.01f)
        {            
            TargetReaching();
        }
    }

    /// <summary>
    /// Initialize the Module
    /// </summary>
    /// <param name="_target">The target to move forward to</param>
    /// <param name="time">The time that the Object take to reach the target </param>
    public void SetTarget(Vector3 _target, float time)
    {
        countdownTime = 0;
        startPos = ObjectToMove.transform.position;
        timeToTarget = time;
        target = _target;
        inTarget = false;
    }

    /// <summary>
    /// Trigger the Move event to move the Object
    /// </summary>
    public void StartMoving()
    {
        isMoving = true;
        OnStartMoving?.Invoke();
    }

    /// <summary>
    /// Stop the movement and trigger the event OnEndMoving
    /// </summary>
    public void TargetReaching()
    {
        inTarget = true;
        isMoving = false;

        OnEndMoving?.Invoke();
    }
}
