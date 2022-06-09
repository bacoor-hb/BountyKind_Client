using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAction : MonoBehaviour
{
    public delegate void OnEventTrigger();
    public OnEventTrigger OnEndLoadingAction;

    public string actionShortDetail = "Loading Action...";
    public float progressPercent = 0.1f;

    /// <summary>
    /// Initialize the Action
    /// </summary>
    /// <param name="_actionDetail"></param>
    /// <param name="_progressPercent"></param>
    public virtual void Init(string _actionDetail, float _progressPercent)
    {
        actionShortDetail = _actionDetail;
        progressPercent = _progressPercent;
    }

    /// <summary>
    /// This function will be called at Loading Scene.
    /// </summary>
    public virtual void StartAction()
    {
        Debug.Log("[LoadingAction] StartAction: " + actionShortDetail);
    }

    /// <summary>
    /// Call OnEndLoadingAction, then Destroy this component.
    /// </summary>
    public virtual void EndAction()
    {
        Debug.Log("[LoadingAction] EndAction: " + actionShortDetail);
        OnEndLoadingAction?.Invoke();
        Destroy(this);
    }
}
