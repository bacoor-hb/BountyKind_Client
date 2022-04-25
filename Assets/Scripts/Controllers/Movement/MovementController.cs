using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public delegate void OnEventCalled();
    public OnEventCalled OnStartMoving;
    public OnEventCalled OnEndMoving;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 target;
    private float time;
    private float timeToTarget;
    private bool inTarget = true;
    private bool isMoving = false;

    private void Update()
    {
        if (isMoving)
        {
            time += Time.deltaTime / timeToTarget;
            transform.position = Vector3.Lerp(startPos, target, time);
            transform.LookAt(target);
        }

        if (!inTarget && transform.position.Equals(target))
        {
            this.inTarget = true;
            this.isMoving = false;
            OnEndMoving?.Invoke();
        }
    }
    public void SetTarget(Vector3 target, float time)
    {
        this.time = 0;
        this.startPos = transform.position;
        this.timeToTarget = time;
        this.target = target;
        this.inTarget = false;
    }

    public void StartMoving()
    {
        this.isMoving = true;
        OnStartMoving?.Invoke();
    }
}
