using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colyseus.Schema;

public class MessageSchemas
{
    public void getMessageSchemaByType(string messageType)
    {
        var messageSchema = new Schema();
        switch(messageType)
        {
            case MESSAGE_TYPE.ROLL_RESULT:
                messageSchema = new RollResultMessage();
                break;
            default:
                break;
        }
        return messageSchema;
    }
}
