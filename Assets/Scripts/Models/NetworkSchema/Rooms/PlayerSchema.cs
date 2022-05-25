// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.34
// 

using Colyseus.Schema;

public partial class PlayerSchema : Schema {
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

	[Type(8, "number")]
	public float yuPoint = default(float);

	[Type(9, "array", typeof(ArraySchema<ItemSchema>))]
	public ArraySchema<ItemSchema> itemListEarned = new ArraySchema<ItemSchema>();

	[Type(10, "number")]
	public float yuEarned = default(float);
}

