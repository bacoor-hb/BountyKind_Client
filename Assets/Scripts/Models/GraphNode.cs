using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    public int NodeID
    {
        get; private set;       
    }

    [SerializeField]
    private GraphNode preNode;
    [SerializeField]
    public GraphNode nextNode;

    /// <summary>
    /// Get the next Node of the graph
    /// </summary>
    /// <returns></returns>
    public GraphNode Next()
    {
        return nextNode;
    }

    /// <summary>
    /// Get the previous node of the graph
    /// </summary>
    /// <returns></returns>
    public GraphNode Previous()
    {
        return preNode;
    }

    /// <summary>
    /// Get the position of this node in the Scene.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetNodeWorldPosition()
    {
        return transform.position;
    }
}
