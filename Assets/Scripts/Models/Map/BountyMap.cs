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
    public int preNode;
    public int nextNode;
}
[Serializable]
public class BountyMap
{
    public string key;
    public string name;
    public int totalNode;
    public List<MapNode> nodes;
}

[Serializable]
public class MapNode_Short
{
    public string key;
    public string name;
    public int totalNode;
}

[Serializable]
public class BountyMap_List
{
    public List<MapNode_Short> maps;
}