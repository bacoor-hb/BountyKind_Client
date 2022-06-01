using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy
{
    public string name { get; set; }
    public string key { get; set; }
    public int atk { get; set; }
    public int def { get; set; }
    public int hp { get; set; }
    public int speed { get; set; }
    public int level { get; set; }
    public int position { get; set; }

    public Enemy(CharacterSchema character)
    {
        name = character.name;
        key = character.key;
        atk = Mathf.RoundToInt(character.atk);
        def = Mathf.RoundToInt(character.def);
        hp = Mathf.RoundToInt(character.hp);
        speed = Mathf.RoundToInt(character.speed);
        level = Mathf.RoundToInt(character.level);
        position = Mathf.RoundToInt(character.position);
    }
}
