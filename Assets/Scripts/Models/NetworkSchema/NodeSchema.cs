// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.34
// 

using Colyseus.Schema;

public partial class NodeSchema : Schema {
	[Type(0, "string")]
	public string key = default(string);

	[Type(1, "string")]
	public string name = default(string);

	[Type(2, "number")]
	public float position = default(float);

	[Type(3, "string")]
	public string type = default(string);

	[Type(4, "array", typeof(ArraySchema<CharacterSchema>))]
	public ArraySchema<CharacterSchema> enemy = new ArraySchema<CharacterSchema>();

	[Type(5, "array", typeof(ArraySchema<float>), "number")]
	public ArraySchema<float> preNode = new ArraySchema<float>();

	[Type(6, "array", typeof(ArraySchema<float>), "number")]
	public ArraySchema<float> nextNode = new ArraySchema<float>();
}

