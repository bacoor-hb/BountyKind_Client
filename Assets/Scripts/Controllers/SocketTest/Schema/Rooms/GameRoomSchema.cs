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

	[Type(1, "number")]
	public float currentPlayer = default(float);

	[Type(2, "boolean")]
	public bool isWaiting = default(bool);
}

