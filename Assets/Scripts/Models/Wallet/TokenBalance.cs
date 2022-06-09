using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenBalance
{
    public int YU_Point;
    public int Energy;

    public TokenBalance()
    {
        YU_Point = 0;
        Energy = 0;
    }
    public TokenBalance(int _YUPoint, int _Energy)
    {
        YU_Point = _YUPoint;
        Energy = _Energy;
    }
}