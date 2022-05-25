using Colyseus.Schema;

public class RollResultMessage : Schema
{
    [Colyseus.Schema.Type(0, "int")]
    public int positionId = 0;

    [Colyseus.Schema.Type(1, "int")]
    public int currentNode = 0;

    [Colyseus.Schema.Type(2, "int")]
    public int totalStep = 0;

    [Colyseus.Schema.Type(3, "int")]
    public int rollResult = 0;

    [Colyseus.Schema.Type(4, "int")]
    public int remainingRoll = 0;
}
