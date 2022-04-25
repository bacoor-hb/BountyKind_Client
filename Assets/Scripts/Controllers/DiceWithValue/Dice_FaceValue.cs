using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_FaceValue : MonoBehaviour
{
    [SerializeField] private int value;
    private bool onGround = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            onGround = false;
        }
    }

    public bool Onground()
    {
        return this.onGround;
    }

    public int Value ()
    {
        return this.value;
    }
}
