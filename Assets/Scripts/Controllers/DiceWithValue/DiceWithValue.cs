using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceWithValue : MonoBehaviour
{
    public delegate void OnEventCalled<T>(T data);
    public OnEventCalled<int> OnValueChange;
    Animator animator;
    private bool thrown = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("diceAnimation", 0);
    }

    public void RollDice(int value)
    {
        if (!thrown)
        {
            StartCoroutine(RolldiceAnimation(value));
        }
    }

    IEnumerator RolldiceAnimation(int value)
    {
        thrown = true;
        animator.SetInteger("diceAnimation", GetDiceAnimation(value));
        yield return new WaitForSeconds(5);
        animator.SetInteger("diceAnimation", 0);
        thrown = false;
        OnValueChange?.Invoke(value);
    }

    int GetDiceAnimation (int value)
    {
        return value * 10 + (Random.Range(1, 3));
    }
}
