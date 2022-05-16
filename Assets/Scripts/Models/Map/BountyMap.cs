using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapNode
{
    public string name;
    public int position;
    public string type;
    public List<Enemy> emeny;
}
[Serializable]
public class BountyMap
{
    public string _id;
    public string key;
    public string name;
    public int totalNode;
    public List<MapNode> nodes;
}
