// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.34
// 

using Colyseus.Schema;

public partial class ItemSchema : Schema {
	[Type(0, "string")]
	public string _id = default(string);

	[Type(1, "string")]
	public string name = default(string);

	[Type(2, "number")]
	public float atk = default(float);

	[Type(3, "number")]
	public float def = default(float);

	[Type(4, "number")]
	public float speed = default(float);

	[Type(5, "number")]
	public float hp = default(float);
}
