using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleModel
{
    public enum FactionSide
    {
        OurSide,
        OpposingSide,
    };

    public enum TypeAttack
    {
        Attack,
        Buff,
        Debuff,
        Passive,
    };
}

[SerializeField]
public class Battle_MSG
{
    public bool skip;
    public float status;
    public AttackResult_MSG[] battleProgress;
}

[SerializeField]
public class AttackResult_MSG
{
    public CharacterInBattle_MSG attacker;
    public CharacterInBattle_MSG target;
    public float turn;
    public float order;
    public string type;
}

[SerializeField]
public class CharacterInBattle_MSG
{
    public string id;
    public string faction;
}
