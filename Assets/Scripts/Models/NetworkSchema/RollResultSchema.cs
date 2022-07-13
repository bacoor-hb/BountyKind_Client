// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.35
// 

using Colyseus.Schema;

public partial class RollResultSchema : Schema {
	[Type(0, "number")]
	public float currentNode = default(float);

	[Type(1, "number")]
	public float totalStep = default(float);

	[Type(2, "number")]
	public float totalRoll = default(float);

	[Type(3, "number")]
	public float rollResult = default(float);
}

