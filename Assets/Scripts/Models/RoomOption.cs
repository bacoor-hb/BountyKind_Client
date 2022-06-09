using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOption
{
    public Dictionary<string, object> option;

    public RoomOption(string _token, string _mapKey, string _gameId = "", string[] _characterIds = null)
    {
        option = new Dictionary<string, object>
        {
            { "authorization", new { 
                token = "Bearer " + _token 
            } },
            { "data", new{   
                    gameId = _gameId,
                    createGame = new {
                        mapKey = _mapKey,
                        charactersIds = _characterIds
            } } }
        };
    }
}
