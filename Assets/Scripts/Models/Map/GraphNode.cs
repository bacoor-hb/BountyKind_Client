using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    public int NodeID;

    public List<GraphNode> preNode;
    public List<GraphNode> nextNode;

    /// <summary>
    /// Get the first next Node of the graph
    /// </summary>
    /// <returns></returns>
    public GraphNode Next()
    {
        return nextNode[0];
    }

    /// <summary>
    /// Get the first previous node of the graph
    /// </summary>
    /// <returns></returns>
    public GraphNode Previous()
    {
        return preNode[0];
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
