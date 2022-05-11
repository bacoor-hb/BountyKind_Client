using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphEventManager
{
    public delegate void OnEventTrigger(params object[] args);
    public OnEventTrigger onEnterNode;
    public OnEventTrigger onEnterStart;
    public void RaiseOnEnterNode(string address, GraphNode node)
    {
        onEnterNode?.Invoke(address, node);
    }

    public void RaiseOnEnterStartNode(string address)
    {
        onEnterStart?.Invoke(address);
    }
}
