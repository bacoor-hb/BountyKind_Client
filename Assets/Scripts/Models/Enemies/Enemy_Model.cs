using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
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
}
