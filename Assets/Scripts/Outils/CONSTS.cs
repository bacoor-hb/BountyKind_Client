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
}

public enum SCENE_NAME
{
    Splash_Scene,
    Menu_Scene,
    Game_Scene,
    Loading_Scene,
    Credit_Scene
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
