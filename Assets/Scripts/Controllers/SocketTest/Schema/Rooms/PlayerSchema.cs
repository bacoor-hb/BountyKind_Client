// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.34
// 

using Colyseus.Schema;

public partial class PlayerSchema : Schema {
	[Type(0, "string")]
	public string address = default(string);

	[Type(1, "string")]
	public string sessionId = default(string);

	[Type(2, "number")]
	public float currentNode = default(float);

	[Type(3, "number")]
	public float totalStep = default(float);
}

