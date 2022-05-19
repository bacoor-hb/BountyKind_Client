// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.34
// 

using Colyseus.Schema;

public partial class GameRoomSchema : Schema {
	[Type(0, "boolean")]
	public bool isStarting = default(bool);

	[Type(1, "array", typeof(ArraySchema<PlayerSchema>))]
	public ArraySchema<PlayerSchema> players = new ArraySchema<PlayerSchema>();

	[Type(2, "number")]
	public float currentIndexPlayer = default(float);

	[Type(3, "boolean")]
	public bool isInteracted = default(bool);

	[Type(4, "boolean")]
	public bool inBattle = default(bool);

	[Type(5, "ref", typeof(BattleSchema))]
	public BattleSchema battleState = new BattleSchema();

	[Type(6, "string")]
	public string name = default(string);

	[Type(7, "array", typeof(ArraySchema<NodeSchema>))]
	public ArraySchema<NodeSchema> nodes = new ArraySchema<NodeSchema>();

	[Type(8, "string")]
	public string image = default(string);
}

