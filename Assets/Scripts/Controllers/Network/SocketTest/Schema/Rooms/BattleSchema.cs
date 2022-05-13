// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.34
// 

using Colyseus.Schema;

public partial class BattleSchema : Schema {
	[Type(0, "array", typeof(ArraySchema<CharacterSchema>))]
	public ArraySchema<CharacterSchema> playerFaction = new ArraySchema<CharacterSchema>();

	[Type(1, "ref", typeof(CharacterSchema))]
	public CharacterSchema monsterFaction = new CharacterSchema();
}

