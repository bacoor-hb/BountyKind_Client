using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum UnitState
{
    STAND_STILL,
    IN_FIGHT_ANIMATE,
    FINISH_FIGHT_ANIMATE,
}
public class UnitController : MonoBehaviour
{
    public delegate void OnEventEnded();
    private OnEventEnded onEndFightAnimation;
    public static OnEventEnded onEndFight;

    [Header ("Movement Controller")]
    [SerializeField]
    private MovementController movementController;
    [SerializeField]
    private float timeToTarget = 1f;

    [Header ("User Data")]  
    public int id;
    [SerializeField]
    private Animator animator;

    private Vector3 rootPos;
    private UnitState state;
    private bool isMoving;
    private void Start()    
    {
        isMoving = false;
        state = UnitState.STAND_STILL;
        rootPos = transform.position;
        movementController.SetObjectToMove(gameObject);
        movementController.OnStartMoving += HandleStartMoving;
        movementController.OnEndMoving += HandEndMoving;
        onEndFightAnimation += HandleEndFightAnimation;
    }

    private void Update()
    {
     
    }
    private void HandleStartMoving()
    {

    }
    private void HandEndMoving()
    {
        if (state != UnitState.FINISH_FIGHT_ANIMATE)
        {
            StartCoroutine(PlayFightAnimation());
        }
        else
        {
            state = UnitState.STAND_STILL;
            onEndFight?.Invoke();
        }
    }

    private IEnumerator PlayFightAnimation()
    {
        state = UnitState.IN_FIGHT_ANIMATE;
        yield return HandleFightAnimation();
        animator.SetBool("Crouch_b", false);
        state = UnitState.FINISH_FIGHT_ANIMATE;
        onEndFightAnimation?.Invoke();
    }


    private string skillName;
    private IEnumerator HandleFightAnimation()
    {
        animator.SetBool(skillName, true);
        yield return new WaitForSeconds(3);
    }

    private void HandleEndFightAnimation()
    {
        movementController.SetTarget(rootPos, timeToTarget);
        movementController.StartMoving();
    }
    public void HandleMove(Vector3 targetPos, string _skillName)
    {
        state = UnitState.STAND_STILL;
        isMoving = true;
        animator.SetFloat("Speed_f", 0.5f);
        skillName = _skillName;
        Vector3 target = new Vector3(targetPos.x, targetPos.y, targetPos.z / 1.1f);
        movementController.SetTarget(target, timeToTarget);
        movementController.StartMoving();
    }

    public void PlayDeathAnimation()
    {
        animator.SetBool("Death_b", true);
    }
}
