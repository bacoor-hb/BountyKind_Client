// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.35
// 

using Colyseus.Schema;

public partial class PlayerSchema : Schema
{
    [Type(0, "string")]
    public string _id = default(string);

    [Type(1, "string")]
    public string username = default(string);

    [Type(2, "string")]
    public string address = default(string);

    [Type(3, "string")]
    public string sessionId = default(string);

    [Type(4, "number")]
    public float currentNode = default(float);

    [Type(5, "number")]
    public float totalStep = default(float);

    [Type(6, "number")]
    public float totalRoll = default(float);

    [Type(7, "number")]
    public float energy = default(float);

    [Type(8, "boolean")]
    public bool isInteracted = default(bool);

    [Type(9, "number")]
    public float yuPoint = default(float);

    [Type(10, "array", typeof(ArraySchema<ItemSchema>))]
    public ArraySchema<ItemSchema> itemListEarned = new ArraySchema<ItemSchema>();


    [Type(11, "array", typeof(ArraySchema<string>), "string")]
    public ArraySchema<string> characterList = new ArraySchema<string>();

    [Type(12, "number")]
    public float yuEarned = default(float);

    [Type(13, "number")]
    public float energyEarned = default(float);
}

