using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph : MonoBehaviour
{
//    public delegate void OnEventTrigger(params object[] args);
//    public OnEventTrigger onEnterNode;
//    public OnEventTrigger onEnterStart;

    protected Dictionary<int, GraphNode> nodes;    
    protected Dictionary<string, GraphNode> currentNodes;

    private UserDataManager UserDataManager;
    #region Initialize
    public void Init()
    {
        nodes = new Dictionary<int, GraphNode>();
        currentNodes = new Dictionary<string, GraphNode>();

        UserDataManager = GlobalManager.Instance.UserDataManager;
    }

    /// <summary>
    /// Add all node to the cache
    /// </summary>
    /// <param name="nodeInScene"></param>
    public void GenerateBoard(GraphNode[] nodeInScene)
    {
        
        BountyMap bountyMap = UserDataManager.GetCurrentMap();
        if(bountyMap == null
            || bountyMap.totalNode != nodeInScene.Length)
        {
            Debug.LogError("[GenerateBoard] Cache Map ERROR...");
            return;
        }

        //Update the next Node and preNode from the Server
        for (int i = 0; i < bountyMap.totalNode; i++)
        {
            int newNextNodeCount = bountyMap.nodes[i].nextNode != null ? bountyMap.nodes[i].nextNode.Count : -1;            
            if(newNextNodeCount > 0)
            {
                nodeInScene[i].nextNode = new List<GraphNode>();
                for (int y = 0; y < newNextNodeCount; y++)
                {
                    int newNextNode = bountyMap.nodes[i].nextNode[y];
                    nodeInScene[i].nextNode.Add(nodeInScene[newNextNode]);
                }
            }
            else
            {
                nodeInScene[i].nextNode = null;
            }

            int newPreNodeCount = bountyMap.nodes[i].preNode != null ? bountyMap.nodes[i].preNode.Count : -1;
            if (newPreNodeCount > 0)
            {
                nodeInScene[i].preNode = new List<GraphNode>();
                for (int y = 0; y < newPreNodeCount; y++)
                {
                    int newPreNode = bountyMap.nodes[i].preNode[y];
                    nodeInScene[i].preNode.Add(nodeInScene[newPreNode]);
                }
            }
            else
            {
                nodeInScene[i].preNode = null;
            }
        }

        //Set the node to the cache
        for (var i = 0; i < nodeInScene.Length; i++)
        {
            nodeInScene[i].NodeID = i;
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
    public void OnEnterNode(string address, GraphNode node)
    {
        if (node != null)
        {
            SetCurrentNodeByAddress(node, address);
        }
       
    }

    private void OnEnterStart(string address)
    {
        
    }
    #endregion
}