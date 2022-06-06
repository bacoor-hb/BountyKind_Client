using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMapsResponse_Short
{
    public List<BountyMap_Short> data;
}

public class GetMapsDetailResponse
{
    public BountyMap data;
}

public class MapNode
{
    public string name;
    public string key;
    public TypeNode type;    
    public List<Enemy> enemies;
    public int position;
    public List<int> preNode;
    public List<int> nextNode;

    public MapNode(Node_MSG node)
    {
        name = node.name;
        key = node.key;
        Enum.TryParse(node.type, out type);
        position = Mathf.RoundToInt(node.position);

        if(node.enemy != null && node.enemy.Length > 0)
        {
            enemies = new List<Enemy>();
            for (int i = 0; i < node.enemy.Length; i++)
            {
                Enemy enemy = new Enemy(node.enemy[i]);
                enemies.Add(enemy);
            }
        }
        
    }
}

public enum TypeNode
{
    START,
    CHANCE,
    LUCKY_DRAW,
    BOSS,
    DEFAULT
}

public class BountyMap
{
    public string key;
    public string name;
    public int totalNode;
    public List<MapNode> nodes;
}

public class BountyMap_Short
{
    public string key;
    public string name;
    public int totalNode;

    public BountyMap_Short(MapShortSchema node)
    {
        key = node.key;
        name = node.name;
        totalNode = Mathf.RoundToInt(node.totalNode);
    }
}