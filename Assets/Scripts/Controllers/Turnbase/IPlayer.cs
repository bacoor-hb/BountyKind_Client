using System;
using UnityEngine;

public abstract class IPlayer: MonoBehaviour
{
    public int id;

    public abstract void StartTurn();
    public abstract void EndTurn();
}
