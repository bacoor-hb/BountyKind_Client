using Colyseus.Schema;

public class RollResultMessage : Schema
{
    [Colyseus.Schema.Type(0, "int")]
    public int id = 0;

    [Colyseus.Schema.Type(1, "int")]
    public int currentNode = 0;

    [Colyseus.Schema.Type(2, "int")]
    public int numberStep = 0;

    [Colyseus.Schema.Type(3, "int")]
    public int step = 0;
}
