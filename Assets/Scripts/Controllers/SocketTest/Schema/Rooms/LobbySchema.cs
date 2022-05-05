// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.34
// 

using Colyseus.Schema;

public partial class LobbySchema : Schema {
	[Type(0, "map", typeof(MapSchema<PlayerSchema>))]
	public MapSchema<PlayerSchema> players = new MapSchema<PlayerSchema>();
}

