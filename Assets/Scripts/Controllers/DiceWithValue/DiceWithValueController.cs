using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceWithValueController : MonoBehaviour
{
    TransformValue initTransform;
    public DiceWithValue dice;

    private void Start()
    {
        Init();
    }

    public void Init(Transform dicePos = null)
    {
        if (dicePos != null)
        {
            initTransform = new TransformValue(dicePos);
            Vector3 pos = new Vector3(initTransform.position.x, transform.position.y, initTransform.position.z);
            transform.position = pos;
        }
        else
        {
            initTransform = new TransformValue(transform);
        }
    }
} 
