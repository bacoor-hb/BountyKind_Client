using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GetDiceAnimation : MonoBehaviour
{
    Rigidbody rb;
    private bool hasLanded = false;
    private bool thrown = false;

    [SerializeField]
    private float x;

    [SerializeField]
    private float y;

    [SerializeField]
    private float z;

    TransformValue initTransform;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        rb = GetComponent<Rigidbody>();
        initTransform = new TransformValue(this.transform);
    }

    private void Update()
    {
        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            thrown = false;
            hasLanded = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.RollDice();
        }
    }

    public void RollDice()
    {
        if (!thrown)
        {
            rb.useGravity = true;
            transform.position = initTransform.position;
            transform.rotation = initTransform.rotation;
            hasLanded = false;
            thrown = true;
            rb.AddTorque(x, y, z);
        }
    }
}