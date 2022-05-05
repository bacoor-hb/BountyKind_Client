using System;
using UnityEngine;

public abstract class IPlayer: MonoBehaviour
{
    [HideInInspector]
    public int id;

    public abstract void StartTurn();
    public abstract void EndTurn();
}
