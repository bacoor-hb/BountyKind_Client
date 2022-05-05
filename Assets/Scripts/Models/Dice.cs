using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dice : MonoBehaviour
{
    public delegate void OnEventCalled<T>(T data);
    public OnEventCalled<int> OnValueChange;

    Rigidbody rb;
    private bool hasLanded = false;
    private bool thrown = false;

    TransformValue initTransform;

    private int diceValue;

    [SerializeField]
    private Dice_FaceValue[] diceValues;

    private void Start()
    {
        Init(transform);
    }

    /// <summary>
    /// Initialize the Dice
    /// </summary>
    /// <param name="dicePos"></param>
    public void Init(Transform dicePos = null)
    {
        rb = GetComponent<Rigidbody>();

        if (dicePos != null)
        {
            initTransform = new TransformValue(dicePos);
        }
        else
        {
            initTransform = new TransformValue(transform);
        }
        
        //rb.useGravity = false;
        diceValue = -1;
    }

    private void Update()
    {
        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            thrown = false;
            hasLanded = true;
            //rb.useGravity = false;
            DiceValueCheck();
        }
    }

    public void RollDice()
    {
        if (!thrown)
        {
            transform.position = initTransform.position;
            hasLanded = false;
            thrown = true;
            //rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }
    }

    void DiceValueCheck()
    {
        diceValue = -1;
        foreach (Dice_FaceValue dice in diceValues)
        {
            if (dice.Onground())
            {
                diceValue = dice.Value();
                OnValueChange?.Invoke(diceValue);
            }
        }
    }
}

public class TransformValue
{
    public Vector3 position;
    public Quaternion rotation;

    public TransformValue()
    {
        position = Vector3.zero;
        rotation = Quaternion.identity;
    }

    public TransformValue(Transform transform)
    {
        position = transform.position;
        rotation = transform.rotation;
    }
}
