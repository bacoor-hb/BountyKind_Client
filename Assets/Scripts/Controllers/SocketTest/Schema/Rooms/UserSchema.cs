// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.34
// 

using Colyseus.Schema;

public partial class UserSchema : Schema {
	[Type(0, "string")]
	public string username = default(string);

	[Type(1, "string")]
	public string address = default(string);

	[Type(2, "string")]
	public string sessionId = default(string);

	[Type(3, "boolean")]
	public bool connected = default(bool);

	[Type(4, "number")]
	public float timestamp = default(float);
}

