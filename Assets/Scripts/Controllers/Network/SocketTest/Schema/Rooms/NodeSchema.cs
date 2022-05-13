// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.34
// 

using Colyseus.Schema;

public partial class NodeSchema : Schema {
	[Type(0, "string")]
	public string name = default(string);

	[Type(1, "number")]
	public float position = default(float);

	[Type(2, "string")]
	public string type = default(string);

	[Type(3, "array", typeof(ArraySchema<CharacterSchema>))]
	public ArraySchema<CharacterSchema> monster = new ArraySchema<CharacterSchema>();

	[Type(4, "string")]
	public string image = default(string);
}

