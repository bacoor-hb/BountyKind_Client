using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph : MonoBehaviour
{
    protected Dictionary<int, GraphNode> nodes;
    
    protected Dictionary<string, GraphNode> currentNodes;

    private GraphEventManager eventManager;

    #region Initialize
    public void Init()
    {
        nodes = new Dictionary<int, GraphNode>();
        currentNodes = new Dictionary<string, GraphNode>();

        eventManager = new GraphEventManager();
        eventManager.onEnterNode += OnEnterNode;
        eventManager.onEnterStart += OnEnterStart;
    }

    /// <summary>
    /// Add all node to the cache
    /// </summary>
    /// <param name="nodeInScene"></param>
    public void GenerateBoard(GraphNode[] nodeInScene)
    {
        for (var i = 0; i < nodeInScene.Length; i++)
        {
            nodes.Add(nodeInScene[i].NodeID, nodeInScene[i]);            
        }
    }

    /// <summary>
    /// Initialize the current Node list
    /// </summary>
    /// <param name="usersAddess"></param>
    /// <param name="startNodes"></param>
    /// <returns>
    /// false if the Length of these 2 inputs are differents
    /// or the userAddress List has some same element.
    /// </returns>
    public bool InitUserNodeList(string[] usersAddess, GraphNode[] startNodes)
    {
        currentNodes = new Dictionary<string, GraphNode>();
        if (usersAddess.Length != startNodes.Length)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < usersAddess.Length; i++)
            {
                if (currentNodes.ContainsKey(usersAddess[i]))
                {
                    currentNodes = new Dictionary<string, GraphNode>();
                    return false;
                }

                currentNodes.Add(usersAddess[i], startNodes[i]);
            }
            return true;
        }
    }
    
    /// <summary>
    /// Update the position of the current Node base on it's Address
    /// </summary>
    /// <param name="_node"></param>
    /// <param name="_address"></param>
    /// <returns>false if this address does not exists</returns>
    public bool SetCurrentNodeByAddress(GraphNode _node, string _address)
    {
        if (currentNodes.ContainsKey(_address))
        {
            currentNodes[_address] = _node;
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Get Node/Nodes
    /// <summary>
    /// Get a node of this graph base on its ID
    /// </summary>
    /// <param name="nodeID"></param>
    /// <returns></returns>
    public GraphNode GetNode(int nodeID)
    {
        return nodes[nodeID];
    }

    /// <summary>
    /// Get a list of all Node in this graph
    /// </summary>
    /// <returns></returns>
    public List<GraphNode> GetNodeList()
    {
        return nodes.Select(x => x.Value).ToList();
    }

    /// <summary>
    /// Get a list of node between currentNode and targetNode, this list include the target node
    /// </summary>
    /// <param name="currentNode"></param>
    /// <param name="targetNode"></param>
    /// <param name="isClockWise"></param>
    /// <returns></returns>
    public List<GraphNode> GetNodesByTargetNode(GraphNode currentNode, GraphNode targetNode, bool isClockWise = true)
    {
        List<GraphNode> listNodes = new List<GraphNode>();
        if (isClockWise)
        {
            GraphNode nextNode = currentNode.Next();
            while (nextNode.NodeID != targetNode.NodeID)
            {
                listNodes.Add(nextNode);
                nextNode = nextNode.Next();
            }
        }
        else
        {
            GraphNode prevNode = currentNode.Previous();
            while (prevNode.NodeID != targetNode.NodeID)
            {
                listNodes.Add(prevNode);
                prevNode = prevNode.Previous();
            }
        }

        listNodes.Add(targetNode);
        return listNodes;
    }

    /// <summary>
    /// Get a list of node "step" step from the current node
    /// </summary>
    /// <param name="currentNode"></param>
    /// <param name="step">Number of step: [> 0 ~ use Next()] [< 0 ~ use Previous()]</param>
    /// <returns></returns>
    public List<GraphNode> GetNodesByStep(GraphNode currentNode, int step)
    {
        List<GraphNode> listNodes = new List<GraphNode>();
        if (step > 0)
        {
            int count = 0;
            GraphNode nextNode = currentNode;
            while (count < step)
            {
                nextNode = nextNode.Next();
                listNodes.Add(nextNode);
                count++;
            }
        }
        else if (step < 0)
        {
            int count = 0;
            GraphNode prevNode = currentNode;
            while (count < step * -1)
            {
                prevNode = prevNode.Previous();
                listNodes.Add(prevNode);
                count++;
            }
        }
        return listNodes;
    }

    /// <summary>
    /// Get the node that the user is step on, using the user address
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public GraphNode GetCurrentNodeByAddress(string address)
    {
        return currentNodes[address];
    }
    #endregion

    #region Node Event
    /// <summary>
    /// Trigger this Event while enter a node
    /// </summary>
    /// <param name="args"></param>
    private void OnEnterNode(params object[] args)
    {
        if (args.Length != 2)
        {
            Debug.LogError("[GetOnEnterNode] Invalid Args...");
        }
        try
        {
            //Convert Params
            string address = args[0].ToString();
            GraphNode node = (GraphNode)args[1];

            if (node != null)
            {
                SetCurrentNodeByAddress(node, address);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("[GetOnEnterNodeERROR] " + ex.Message);
        }
    }

    private void OnEnterStart(params object[] args)
    {
        if (args.Length != 1)
        {
            Debug.LogError("[GetOnEnterNode] Invalid Args...");
        }
        try
        {
            //Convert Params
            string address = args[0].ToString();
            Debug.Log("Bat dau");
        }
        catch (Exception ex)
        {
            Debug.LogError("[GetOnEnterNodeERROR] " + ex.Message);
        }
    }
    #endregion
}