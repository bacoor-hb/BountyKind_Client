using System;
using UnityEngine;

[Serializable]
public enum CharacterStatus 
{
    ACTIVE,
    ONSALE,
    ONRENT,
    DESTROYED,
};

public enum GameCharacterRace
{
    HUMANOID,
    BEAST,
    GOD,
    DEMON,
    MACHINE,
};

public enum GameCharacterElement
{
    IGNIS,
    PLANT,
    ANIMA,
    EARTH,
    ELEKI,
    AQUA,
};

public enum GameCharacterRarity
{
    COMMON,
    UNCOMMON,
    RARE,
    EPIC,
    LEGENDARY,
    EMPEROR
};

[Serializable]
public class NFTItem
{
    public string addressContract;
    public string nftId;
}

[Serializable]
public class CharacterBaseStat
{
    public string name;
    public string key;
    public int baseAtk;
    public int baseDef;
    public int baseSpeed;
    public int baseHp;
    public int level;
    public int position;

    public CharacterBaseStat(CharacterSchema character)
    {
        name = character.name;
        key = character.key;
        baseAtk = Mathf.RoundToInt(character.atk);
        baseDef = Mathf.RoundToInt(character.def);
        baseHp = Mathf.RoundToInt(character.hp);
        baseSpeed = Mathf.RoundToInt(character.speed);
        level = Mathf.RoundToInt(character.level);
    }
}

[Serializable]
public class Character
{
    public CharacterBaseStat baseStat;
    public int atk;
    public int def;
    public int speed;
    public int hp;
    
    public int exp;
    

    public NFTItem[] itemList;
    public CharacterStatus status;
    public long updatedAt;
    public long createdAt;
    public string nftId;

    public string addressContract;
    public string ownerAddress;
    public string renterAddress;
    public long rentAt;
    public long updatedOwnerAt;

    public bool inGame;
}

[Serializable]
public class Status_MSG
{
    public float atk;
    public float def;
    public float speed;
    public float hp;
}

[Serializable]
public class Character_MSG : Status_MSG
{
    public string key;
    public string name;
    public float position;
    public float level;
}
