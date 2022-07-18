using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LocalGameController
{
    #region Movement Controller  
    /// <summary>
    /// Order the current player to Move
    /// </summary>
    private void Move_CurrentPlayer()
    {
        int _moveValue = DicesValue[0];
        int _userToMove = currentPlayerId;

        //Deactive all Buttons so the player cannot stack any action until the move end.
        LocalGameView.DeactiveAllPopup();

        MovePlayer(_moveValue, _userToMove);
    }
    /// <summary>
    /// Order the player to Move
    /// </summary>
    /// <param name="_moveValue">The number of Math to move</param>
    /// <param name="_userToMove">Id of the user to Move in the Players List</param>
    void MovePlayer(int _moveValue, int _userToMove)
    {
        GraphNode currentNode = board.GetCurrentNodeByAddress(GetUserAddress(_userToMove));
        targetNodes = board.GetNodesByStep(currentNode, _moveValue);

        if (targetNodes != null && targetNodes.Count > 0)
        {
            _targetsPos = new Vector3[targetNodes.Count];
            for (int i = 0; i < targetNodes.Count; i++)
            {
                _targetsPos[i] = targetNodes[i].GetNodeWorldPosition();
            }

            currentTarget = 0;

            //Register Move Action for this turn
            MovementController.OnStartMoving = null;
            MovementController.OnStartMoving += StartMoving1Math;
            MovementController.OnEndMoving = null;
            MovementController.OnEndMoving += EndMoving1Math;
            MoveTheObject();
        }
        else
        {
            return;
        }
    }

    int currentTarget = 0;
    Vector3[] _targetsPos;
    List<GraphNode> targetNodes;
    float timeToTarget = 0.5f;

    /// <summary>
    /// Move the object to the targets in the _targetList. At the last target, when the move action finish, end the Turn base Action
    /// </summary>
    void MoveTheObject()
    {
        if (currentTarget < _targetsPos.Length)
        {
            MovementController.SetTarget(_targetsPos[currentTarget], timeToTarget);
            currentTarget++;
            MovementController.StartMoving();
        }
        else
        {
            TurnBaseController.EndAction();
        }
    }

    private void StartMoving1Math()
    {
        Debug.Log("[StartMoving1Math] On Move Start: " + currentTarget);
    }

    private void EndMoving1Math()
    {
        Debug.Log("[EndMoving1Math] On Move End: " + currentTarget);
        board.OnEnterNode(GetUserAddress(currentPlayerId), targetNodes[currentTarget - 1]);
        MoveTheObject();
    }

    private void EndMovingAllTarget()
    {
        Debug.Log("[LocalGameController] EndMovingAllTarget, then TriggerMathEffect");
        //OnTriggerMathEffect();
        Multiplayer_GameEvent.HandleCheckInteracted();
    }

    #endregion
}
