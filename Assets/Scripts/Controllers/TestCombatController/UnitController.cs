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
    public delegate void OnUpdateHealthEvent(string characterId, int newHealth);
    public OnUpdateHealthEvent OnUpdateHealth;
    public delegate void OnEventEnded();
    private OnEventEnded onEndFightAnimation;
    public static OnEventEnded onEndFight;
    public delegate void OnBeingAttackedEvent(string characterId);
    public OnBeingAttackedEvent OnBeingAttacked;
    public OnBeingAttackedEvent OnFinishBeingAttacked;
    [SerializeField]
    private FightSceneUnitView fightSceneUnitView;

    [Header("Movement Controller")]
    [SerializeField]
    private MovementController movementController;
    [SerializeField]
    private float timeToTarget;

    [Header("User Data")]
    public int id;
    public FormationCharacters characterInfo;
    public int currentHealth;
    public BattleUnit target;
    [SerializeField]
    private Animator animator;

    private Vector3 rootPos;
    private UnitState state;
    private Vector3 targetPos;
    public void Init(FormationCharacters _characterInfo)
    {
        characterInfo = _characterInfo;
        currentHealth = characterInfo.hp;
        timeToTarget = 1f;
        //Debug.Log("[UnitController]: Start (45)");
        animator = GetComponentInChildren<Animator>();
        //animator.SetInteger("MeleeType_int", 0);
        state = UnitState.STAND_STILL;
        rootPos = transform.position;
        movementController.SetObjectToMove(gameObject);
        movementController.OnStartMoving = null;
        movementController.OnStartMoving += HandleStartMoving;
        movementController.OnEndMoving = null;
        movementController.OnEndMoving += HandEndMoving;
        onEndFightAnimation = null;
        onEndFightAnimation += HandleEndFightAnimation;
        OnBeingAttacked = null;
        OnBeingAttacked += HandleOnBeingAttacked;
        OnFinishBeingAttacked += HandleFinishOnBeingAttacked;
        //Debug.Log("[UnitController]: Start (56)");
    }

    private void Update()
    {
        float _floatCurrentHealth = currentHealth;
        float _floatBaseHp = characterInfo.hp;
        float result = _floatCurrentHealth / _floatBaseHp;
        fightSceneUnitView.SetSliderValue(result);
        string healthText = currentHealth + " / " + characterInfo.hp;
        fightSceneUnitView.SetHealthText(healthText);
        if (result > 0.7f)
        {
            fightSceneUnitView.SetSliderColor(Color.green);
        }
        else if (result <= 0.7f && result > 0.5f)
        {
            fightSceneUnitView.SetSliderColor(Color.yellow);
        }
        else if (result <= 0.5f && result > 0.25f)
        {
            fightSceneUnitView.SetSliderColor(Color.yellow);
        }
        else
        {
            fightSceneUnitView.SetSliderColor(Color.red);
        }

        if (currentHealth == 0)
        {
            PlayDeathAnimation();
        }
    }
    private void HandleStartMoving()
    {

    }

    void HandleOnBeingAttacked(string characterId)
    {
        if (characterInfo._id == characterId)
        {
            GetComponentInChildren<Renderer>().material.color = new Color32(250, 128, 114, 1);
            animator.SetFloat("Speed_f", 0f);
            animator.SetBool("Jump_b", true);
        }
    }
    void HandleFinishOnBeingAttacked(string characterId)
    {
        if (characterInfo._id == characterId)
        {
            GetComponentInChildren<Renderer>().material.color = Color.white;
            animator.SetFloat("Speed_f", 0.5f);
            animator.SetInteger("Animation_int", 0);
        }

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
            animator.SetFloat("Speed_f", 0f);
            fightSceneUnitView.SetOutlineWidth(0f);
            transform.LookAt(targetPos);
            onEndFight?.Invoke();
        }
    }

    private IEnumerator PlayFightAnimation()
    {
        state = UnitState.IN_FIGHT_ANIMATE;
        yield return HandleFightAnimation();
        OnFinishBeingAttacked?.Invoke(this.target._id);
        animator.SetInteger("WeaponType_int", 0);
        state = UnitState.FINISH_FIGHT_ANIMATE;
        onEndFightAnimation?.Invoke();
    }

    private IEnumerator HandleFightAnimation()
    {
        animator.SetFloat("Speed_f", 0f);
        animator.SetInteger("WeaponType_int", 12);
        OnBeingAttacked?.Invoke(this.target._id);
        yield return new WaitForSeconds(1);
    }

    private void HandleEndFightAnimation()
    {
        animator.SetFloat("Speed_f", 0.5f);
        OnUpdateHealth(this.target._id, this.target.hp);
        movementController.SetTarget(rootPos, timeToTarget);
        movementController.StartMoving();
    }
    public void HandleMove(Vector3 _targetPos, BattleProgess battleData)
    {
        float distance = Vector3.Distance(transform.position, _targetPos);
        timeToTarget = distance / 100f;
        targetPos = _targetPos;
        fightSceneUnitView.SetOutlineWidth(5f);
        this.target = battleData.target;
        state = UnitState.STAND_STILL;
        animator.SetFloat("Speed_f", 0.5f);
        Vector3 target = new(_targetPos.x, _targetPos.y, _targetPos.z / 1.1f);
        movementController.SetTarget(target, timeToTarget);
        movementController.StartMoving();
    }

    public void PlayDeathAnimation()
    {
        animator.SetBool("Death_b", true);
    }

    private void OnDestroy()
    {
        movementController.OnStartMoving -= HandleStartMoving;
        movementController.OnEndMoving -= HandEndMoving;
        onEndFightAnimation -= HandleEndFightAnimation;
        OnBeingAttacked -= HandleOnBeingAttacked;
        OnFinishBeingAttacked -= HandleFinishOnBeingAttacked;
    }
}
