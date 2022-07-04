using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenBalance
{
    public int yuPoint;
    public int energy;
    public int yuEarned;
    public int energyEarned;

    public TokenBalance()
    {
        yuPoint = 0;
        energy = 0;
        yuEarned = 0;
        energyEarned = 0;
    }
    public TokenBalance(TokenBalance_MSG balance)
    {
        yuPoint = Mathf.RoundToInt(balance.yuPoint);
        energy = Mathf.RoundToInt(balance.energy);
        yuEarned = Mathf.RoundToInt(balance.yuEarned);
        energyEarned = Mathf.RoundToInt(balance.energyEarned);
    }
}

[SerializeField]
public class TokenBalance_MSG
{
    public float yuPoint;
    public float energy;
    public float yuEarned;
    public float energyEarned;
}