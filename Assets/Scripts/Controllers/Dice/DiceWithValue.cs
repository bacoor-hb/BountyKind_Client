using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceWithValue : MonoBehaviour
{
    public delegate void OnEventCalled<T>(T data);
    public OnEventCalled<int> OnFinishAnim;

    [SerializeField]
    Animator anim;
    [SerializeField]
    Transform DiceTransform;
    [SerializeField]
    float fallSpd = 5;
    Vector3 target;
    private bool isThrowing = false;

    /// <summary>
    /// Initialize the dice.
    /// </summary>
    public void Init()
    {
        ResetDice();
    }

    /// <summary>
    /// Set the Dice to the Spawn pos, then deativate it.
    /// </summary>
    public void ResetDice()
    {
        anim.SetTrigger(CONSTS.DICE_ANIM_PARAM_DICE_TRIGGER_STATE_CHANGE);

        //Reset Dice transform
        DiceTransform.localPosition = Vector3.zero;
        DiceTransform.localRotation = Quaternion.identity;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hit))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Debug.Log("Did Hit the ground: " + hit.point);
            target = hit.point;
        }
        else
        {
            Debug.Log("Ground not found...");

            //Set a tmp target position when no ground was found -> avoid infinite falling. 
            target = transform.position;
            target.y = 0.3f;
        }

        isThrowing = false;
        gameObject.SetActive(false);
    }

    int diceValue;
    /// <summary>
    /// Call this function to roll the dice
    /// </summary>
    /// <param name="value">The value that we want to show on the dice (Limit from 1 to 6)</param>
    public void RollDice(int value)
    {
        diceValue = Mathf.Clamp(value, CONSTS.DICE_VALUE_MIN, CONSTS.DICE_VALUE_MAX);
        //Debug.Log("[RollDice] Value = " + diceValue);
        if (!isThrowing)
        {
            gameObject.SetActive(true);            
            isThrowing = true;

            Debug.Log("Trigger the Animation...");
            StartCoroutine(RolldiceAnimation(diceValue));
        }
    }

    /// <summary>
    /// Coroutine to animate the roll dice animation
    /// </summary>
    /// <param name="value">The value that we want to show on the dice (Limit from 1 to 6)</param>
    /// <returns></returns>
    IEnumerator RolldiceAnimation(int value)
    {
        yield return new WaitForEndOfFrame();

        while(Vector3.Distance(transform.position, target) > 0.3f)
        {
            transform.Translate(Vector3.down * Time.deltaTime * fallSpd);
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Rolling Dice with Value: " + value);
        int blendId = GetRandomDiceAnimId();
        anim.SetInteger(CONSTS.DICE_ANIM_PARAM_DICE_BLEND, blendId);
        anim.SetInteger(CONSTS.DICE_ANIM_PARAM_DICE_VALUE, value);
        anim.SetTrigger(CONSTS.DICE_ANIM_PARAM_DICE_TRIGGER_STATE_CHANGE);

        yield return new WaitForSeconds(CONSTS.DICE_ANIM_TIME_MAX);
        isThrowing = false;
        OnFinishAnim?.Invoke(value);
    }

    /// <summary>
    /// Get a random dice animation clip
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    int GetRandomDiceAnimId ()
    {
        //int clipId = Random.Range(CONSTS.DICE_ANIM_TYPE_MIN, CONSTS.DICE_ANIM_TYPE_MAX);
        int clipId = Random.Range(CONSTS.DICE_ANIM_TYPE_MIN, CONSTS.DICE_ANIM_TYPE_MIN);
        return clipId;
    }    
}
