using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        if(node.nextNode.Length > 0)
        {
            nextNode = new List<int>();
            for(int i = 0; i < node.nextNode.Length; i++)
            {
                nextNode.Add(node.nextNode[i]);
            }
        }

        if (node.preNode.Length > 0)
        {
            preNode = new List<int>();
            for (int i = 0; i < node.preNode.Length; i++)
            {
                preNode.Add(node.preNode[i]);
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

[Serializable]
public class MapShortList_MSG
{
    public MapShort_MSG[] maps;
}

[Serializable]
public class MapShort_MSG
{
    public string key;
    public string name;
    public int totalNode;

    public MapShort_MSG(string _key, string _name, float _totalNode)
    {
        key = _key;
        name = _name;
        totalNode = Mathf.RoundToInt(_totalNode);
    }

    public MapShort_MSG(MapShortSchema node)
    {
        key = node.key;
        name = node.name;
        totalNode = Mathf.RoundToInt(node.totalNode);
    }
}

[Serializable]
public class Map_MSG
{
    public string key;
    public string name;
    public int totalNode;
    public Node_MSG[] nodes;
}

[Serializable]
public class Node_MSG
{
    public string key;
    public string name;
    public int position;
    public string type;
    public Character_MSG[] enemy = { };
    public int[] preNode;
    public int[] nextNode;
}

[Serializable]
public  class RollResult_MSG
{
    public float currentNode;
    public float totalStep;
    public float totalRoll;
    public float rollResult;
}