using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserAnimationController : MonoBehaviour
{
    public delegate void OnEventEnded<T>(T data);

    [SerializeField]
    private Animator anim;
    
    public void Init()
    {

    }   
    
    /// <summary>
    /// Control the Player walking animation
    /// </summary>
    /// <param name="speed">Base on CONSTS value</param>
    public void Walking(float speed)
    {
        anim.SetFloat(CONSTS.ANIM_SPEED_F, speed);
        if(speed >= CONSTS.ANIM_SPEED_RUN)
        {
            anim.SetBool(CONSTS.ANIM_STATIC_B, true);
        }
        else
        {
            anim.SetBool(CONSTS.ANIM_STATIC_B, false);       
        }
    }
}
