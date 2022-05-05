// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.34
// 

using Colyseus.Schema;

public partial class NetworkedUser : Schema {
	[Type(0, "string")]
	public string sessionId = default(string);

	[Type(1, "boolean")]
	public bool connected = default(bool);

	[Type(2, "number")]
	public float timestamp = default(float);

	[Type(3, "ref", typeof(UserInfo))]
	public UserInfo userInfo = new UserInfo();
}

