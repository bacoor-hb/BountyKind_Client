using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TokenBalance
{
    public float YU;
    public float YU2;
    public float FFE;

    public TokenBalance()
    {
        YU = 0;
        YU2 = 0;
        FFE = 0;
    }
    public TokenBalance(float _YU, float _YU2, float _FFE)
    {
        YU = _YU;
        YU2 = _YU2;
        FFE = _FFE;
    }
}