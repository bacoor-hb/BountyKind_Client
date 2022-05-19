using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapNode
{
    public string name;
    public string key;
    public string type;    
    public List<Enemy> emeny;
    public int position;
}
[Serializable]
public class BountyMap
{
    public string key;
    public string name;
    public int totalNode;
    public List<MapNode> nodes;
}
