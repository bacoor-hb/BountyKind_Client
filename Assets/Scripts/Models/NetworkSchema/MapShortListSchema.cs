// 
// THIS FILE HAS BEEN GENERATED AUTOMATICALLY
// DO NOT CHANGE IT MANUALLY UNLESS YOU KNOW WHAT YOU'RE DOING
// 
// GENERATED USING @colyseus/schema 1.0.34
// 

using Colyseus.Schema;

public partial class MapShortListSchema : Schema {
	[Type(0, "array", typeof(ArraySchema<MapShortSchema>))]
	public ArraySchema<MapShortSchema> maps = new ArraySchema<MapShortSchema>();
}

