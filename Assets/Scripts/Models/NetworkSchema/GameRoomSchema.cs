// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.35
// 

using Colyseus.Schema;

public partial class GameRoomSchema : Schema {
	[Type(0, "boolean")]
	public bool isStarting = default(bool);

	[Type(1, "array", typeof(ArraySchema<PlayerSchema>))]
	public ArraySchema<PlayerSchema> players = new ArraySchema<PlayerSchema>();

	[Type(2, "number")]
	public float currentIndexPlayer = default(float);

	[Type(3, "string")]
	public string name = default(string);

	[Type(4, "string")]
	public string mapKey = default(string);
}

