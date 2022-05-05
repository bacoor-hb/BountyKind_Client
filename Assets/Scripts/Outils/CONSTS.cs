public class CONSTS
{
    public const string SCENE_KEY = "SCENE_KEY";
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
    RELEASE_CARD,
    ROLL_DICE,
    RUN_THE_CELL,
    PURCHASE,
    BUILDING,
    AUCTION,
    END_TURN,
}

public class TEXT_UI
{
    public const string LANGUAGE = "Language";
    public const string ENGLISH = "English";
    public const string JAPANESE = "Japanese";
    
}

public enum PROPERTY_ID: int
{
    STREET_0 = 0,
    STREET_1 = 1,
    STREET_2 = 2,
    STREET_3 = 3,
    STREET_4 = 4,
    STREET_5 = 5,
    STREET_6 = 6,
    STREET_7 = 7,
    STREET_8 = 8,
    STREET_9 = 9,
    STREET_10 = 10,
    STREET_11 = 11,
    STREET_12 = 12,
    STREET_13 = 13,
    STREET_14 = 14,
    STREET_15 = 15,
    STREET_16 = 16,
    STREET_17 = 17,
    STREET_18 = 18,
    STREET_19 = 19,
    STREET_20 = 20,
    STREET_21 = 21,

    ELECTRIC_0 = 22,
    ELECTRIC_1 = 23,

    TRANSPORT_0 = 24,
    TRANSPORT_1 = 25,
    TRANSPORT_2 = 26,
    TRANSPORT_3 = 27,
    
    START = -1,

    CHANCE_0 = -2,
    CHANCE_1 = -3,
    CHANCE_2 = -4,

    LUCKY_0 = -5,
    LUCKY_1 = -6,
    LUCKY_2 = -7,

    TAX_0 = -8,
    TAX_1 = -9,

    PARK = -10,

    PRISON = -11,
    IMPRISON = -12,

}

public class ROOM_TYPE
{
    public const string GAME_ROOM = "my_room";
}

public class MESSAGE_TYPE
{
    public const string ROLL_RESULT = "roll_result";
}

