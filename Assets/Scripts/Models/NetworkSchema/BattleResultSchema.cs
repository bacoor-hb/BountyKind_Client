// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.35
// 

using Colyseus.Schema;

public partial class BattleResultSchema : Schema {
	[Type(0, "boolean")]
	public bool skip = default(bool);

	[Type(1, "number")]
	public float status = default(float);

	[Type(2, "array", typeof(ArraySchema<AttackResultSchema>))]
	public ArraySchema<AttackResultSchema> battleProgress = new ArraySchema<AttackResultSchema>();

	[Type(3, "array", typeof(ArraySchema<RewardSchema>))]
	public ArraySchema<RewardSchema> rewards = new ArraySchema<RewardSchema>();
}

