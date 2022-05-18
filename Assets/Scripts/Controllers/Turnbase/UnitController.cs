using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum UnitState
{
    INIT,
    IN_FIGHT_ANIMATE,
    FINISH_FIGHT_ANIMATE,
}
public class UnitController : MonoBehaviour
{
    private delegate void OnEndFightAnimation();
    private OnEndFightAnimation onEndFightAnimation;

    public delegate void OnEndFight();
    public static OnEndFight onEndFight;

    Animator animator;
    [SerializeField]
    private MovementController movementController;
    private Vector3 rootPos;
    [SerializeField]
    public int id;
    [SerializeField]
    private float timeToTarget = 1.5f;
    private UnitState state;
    private string skillName;
    private GameObject barrier;
    private void Start()    
    {
        barrier = GameObject.FindGameObjectWithTag("Barrier");
        state = UnitState.INIT;
        animator = GetComponent<Animator>();
        transform.LookAt(barrier.transform);
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
            transform.LookAt(barrier.transform);
            state = UnitState.INIT;
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
        skillName = _skillName;
        state = UnitState.INIT;
        Vector3 target = new Vector3(targetPos.x, targetPos.y, targetPos.z / 1.5f);
        movementController.SetTarget(target, timeToTarget);
        movementController.StartMoving();
    }
}
