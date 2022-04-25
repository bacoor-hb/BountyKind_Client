using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphEventManager : MonoBehaviour
{
    public delegate void onEventEnter(params object[] args);
    public delegate void onEventEnterStart(params object[] args);
    public delegate void onEventEnterImprison(params object[] args);
    public onEventEnter onEnterNode;
    public onEventEnterStart onEnterStart;
    public onEventEnterImprison onEnterImprison;
    public void RaiseOnEnterNode(string address, GraphNode node)
    {
        if (onEnterNode != null)
        {
            onEnterNode(address, node);
        }
    }

    public void RaiseOnEnterStartNode(string address)
    {
        if (onEnterStart != null)
        {
            onEnterStart(address);
        }
    }

    public void RaiseOnEnterImprison(string address)
    {
        if (onEnterImprison != null)
        {
            onEnterImprison(address);
        }
    }
}
