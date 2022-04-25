using UnityEngine;
using System.Collections;

public class Action : MonoBehaviour
{
    public delegate void Event();
    public Event StartAction;
    public Event EndAction;
    public Event EventAction;

    [SerializeField]
    protected ACTION_TYPE actionType;
    protected int userId;

    protected TurnBaseController turnBaseController;
    public virtual void InitAction(int _userId, TurnBaseController _controller)
    {
        userId = _userId;
        turnBaseController = _controller;
        ClearEvent();
    }

    public ACTION_TYPE GetAction()
    {
        return actionType;
    }

    /// <summary>
    /// Clear all event of this action
    /// </summary>
    public virtual void ClearEvent()
    {
        StartAction = null;
        EndAction = null;
        EventAction = null;
    }

    public virtual void OnStartAction()
    {
        StartAction?.Invoke();
    }

    public virtual void OnEndAction()
    {
        EndAction?.Invoke();
    }
}
