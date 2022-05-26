using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GetMapsResponse_Short
{
    public List<BountyMap_Short> data;
}

[Serializable]
public class GetMapsDetailResponse
{
    public BountyMap data;
}

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
public class BountyMap_Short
{
    public string key;
    public string name;
    public int totalNode;
}