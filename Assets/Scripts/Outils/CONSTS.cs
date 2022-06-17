public class CONSTS
{
    public const string SCENE_KEY = "SCENE_KEY";

    #region LAYER ID
    public const string LAYER_PLANE = "Plane";
    public const string LAYER_DICE = "Dice";
    #endregion

    #region DICE CONST
    public const float DICE_ANIM_TIME_MAX = 3;
    public const int DICE_VALUE_MIN = 1;
    public const int DICE_VALUE_MAX = 6;
    public const int DICE_ANIM_TYPE_MIN = 0;
    public const int DICE_ANIM_TYPE_MAX = 2;

    public const string DICE_ANIM_PARAM_DICE_VALUE = "Dice_Value";
    public const string DICE_ANIM_PARAM_DICE_BLEND = "Dice_Blend";
    public const string DICE_ANIM_PARAM_DICE_TRIGGER_STATE_CHANGE = "ToTriggerStateChange";

    #endregion

    #region NETWORK SETTING
    public const string HOST_ENDPOINT_SOCKET = "wss://dev-game-server.w3w.app/";
    public const string HOST_ENDPOINT_API = "https://dev-game-api.w3w.app/";
    //public const string HOST_ENDPOINT_SOCKET = "ws://192.168.9.5:2567/";
    //public const string HOST_ENDPOINT_API = "http://192.168.9.5:4000/";
    public const string HOST_GET_MAP_API = HOST_ENDPOINT_API + "api/maps";
    public const string HOST_GET_USERDATA_API = HOST_ENDPOINT_API + "api/users/detail";
    #endregion

    #region LOADING DETAIL MESSAGE
    public const string LOADING_DETAIL_LOADMAP = "Initialize the map...";
    #endregion
}

public enum SEND_TYPE
{
    LOBBY_SEND,
    GAMEROOM_SEND
}

public enum SCENE_NAME
{
    SplashScene,
    MainMenu,
    GameScene,
    LoadingScene,
    CreditScene,
}

public enum CYCLE_TURN 
{
    START_TURN,
    WAITING_ACTION,
    START_ACTION,
    END_ACTION,
    END_TURN,
}

public enum ACTION_TYPE
{
    MOVE,
    ROLL_DICE,
    LUCKY_DRAW,
    CHANCE,
    COMBAT,
    END_TURN,

    INVALID_ACTION = 100
}

public class TEXT_UI
{
    public const string LANGUAGE = "Language";
    public const string ENGLISH = "English";
    public const string JAPANESE = "Japanese";
    
}

public class ROOM_TYPE
{
    public const string GAME_ROOM = "my_room";
    public const string LOBBY_ROOM = "lobby";
}

#region SERVER EVENT
public enum GAMEROOM_SENT_EVENTS
{
    ROLL_DICE,
    LUCKY_DRAW,
    FIGHT,
    BALANCE,
    GAME_EXIT,
    CHANCE,
    DEFAULT,
}

public enum GAMEROOM_RECEIVE_EVENTS
{
    ROLL_RESULT,
    LUCKY_DRAW_RESULT,
    FIGHT_RESULT,
    BALANCE_RESULT,
    CHANCE_RESULT,
}

public enum LOBBY_SENT_EVENTS
{
    MAP_LIST,
    MAP_NODE,
};

public enum LOBBY_RECEIVE_EVENTS
{
    MAP_LIST_RESULT,
    MAP_NODE_RESULT,
};

public enum PLAYER_ERRORS
{
    NOT_YOUR_TURN,
    NOT_ENOUGH_ENERGY,
    NOT_ENOUGH_YU_POINT,
    NOT_INTERACTED_WITH_NODE,
    INTERACTED_WITH_NODE,
    SENT_EVENT_WRONG,
    TICKET_INVALID,
    TICKET_NOT_OWNER,
    TICKET_IS_USED,
    INVALID_GAME,
}

public enum REQUEST_ERRORS
{
    USER_NOT_FOUND,
    INVALID_ACCESS_TOKEN,
}

public enum STATUS_BATTLE
{
    WIN = 1,
    DRAW = 0,
    LOSS = -1,
}
#endregion



