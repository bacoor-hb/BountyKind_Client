using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TokenBalance
{
    public float YU;
    public float YU_Point;
    public float FFE;
    public float FFE_Point;

    public TokenBalance()
    {
        YU = 0;
        YU_Point = 0;
        FFE = 0;
        FFE_Point = 0;
    }
    public TokenBalance(float _YU, float _YUPoint, float _FFE, float _FFEPoint)
    {
        YU = _YU;
        YU_Point = _YUPoint;
        FFE = _FFE;
        FFE_Point = _FFEPoint;
    }
}