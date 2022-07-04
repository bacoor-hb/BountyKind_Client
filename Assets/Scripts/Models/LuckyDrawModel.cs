using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class LuckyDraw_MSG
{
    public string name;
    public string status;
    public float atk;
    public float def;
    public float speed;
    public float hp;
}

[SerializeField]
public class Reward_MSG
{
    public int amount;
    public RewardType type;
    public string key;
}

public enum RewardType
{
    yu,
    energy,
    item,
    character,
    sapphire,
}