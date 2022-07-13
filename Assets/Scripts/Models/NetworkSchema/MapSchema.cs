// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.35
// 

using Colyseus.Schema;

public partial class MapSchema : MapShortSchema {
	[Type(3, "array", typeof(ArraySchema<NodeSchema>))]
	public ArraySchema<NodeSchema> nodes = new ArraySchema<NodeSchema>();
}

