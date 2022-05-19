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
    public const string HOST_ENDPOINT_DEFAULT = "wss://dev-game-api.w3w.app";
    //public const string HOST_ENDPOINT_DEFAULT = "ws://192.168.9.4:2567";
    public const string HOST_GET_MAP_API = "https://dev-game-api.w3w.app/api/maps";
    #endregion
}

public enum SCENE_NAME
{
    SplashScene,
    MainMenu,
    GameScene,
    LoadingScene,
    CreditScene,

    Test_Login_Success,
    Test_Create_Room,
    Test_Login,
    Test_Game_Scene
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
    END_TURN,
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

public class PLAYER_RECEIVE_EVENTS
{
    public const string ROLL_RESULT = "roll_result";
    public const string LUCKY_DRAW_RESULT = "lucky_draw_result";
    public const string FIGHT_RESULT = "fight_result";
    public const string BATTLE_INIT = "battle_init";
    public const string ERROR = "error";
}

public class PLAYER_SENT_EVENTS
{
    public const string ROLL_DICE = "roll";
    public const string FIGHT = "fight";
    public const string GAME_EXIT = "exit";
    public const string LUCKY_DRAW = "lucky_draw";

}
public class PLAYER_ERRORS
{
    public const string NOT_YOUR_TURN = "NOT_YOUR_TURN";
}


