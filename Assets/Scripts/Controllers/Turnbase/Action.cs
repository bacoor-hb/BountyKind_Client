using UnityEngine;
using System.Collections;

namespace BountyKind
{
    public class Action : MonoBehaviour
    {
        public delegate void Event();
        public Event StartAction;
        public Event EndAction;
        public Event EventAction;

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
        /// Set Action data to process
        /// </summary>
        public virtual void SetActionData(object args)
        {

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
            Debug.Log("[Action] OnStartAction");
            StartAction?.Invoke();
        }

        public virtual void OnEndAction()
        {
            Debug.Log("[Action] OnEndAction");
            EndAction?.Invoke();
        }
    }
}

