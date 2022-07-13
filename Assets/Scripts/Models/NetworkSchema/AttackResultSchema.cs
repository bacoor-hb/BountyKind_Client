// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.35
// 

using Colyseus.Schema;

public partial class AttackResultSchema : Schema {
	[Type(0, "ref", typeof(CharacterInBattle))]
	public CharacterInBattle attacker = new CharacterInBattle();

	[Type(1, "ref", typeof(CharacterInBattle))]
	public CharacterInBattle target = new CharacterInBattle();

	[Type(2, "number")]
	public float turn = default(float);

	[Type(3, "number")]
	public float order = default(float);

	[Type(4, "string")]
	public string type = default(string);
}

