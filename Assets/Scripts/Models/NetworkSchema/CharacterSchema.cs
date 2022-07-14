// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.34
// 

using Colyseus.Schema;

public partial class CharacterSchema : StatusSchema
{
    [Type(4, "string")]
    public string _id = default(string);

    [Type(5, "string")]
    public string key = default(string);

    [Type(6, "string")]
    public string name = default(string);

    [Type(7, "number")]
    public float position = default(float);

    [Type(8, "number")]
    public float level = default(float);
}