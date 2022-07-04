using System;
using UnityEngine;

[Serializable]
public enum CharacterStatus
{
    active,
    on_sale, 
    on_rent, 
    destroyed, 
    collected,
    in_game,
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

public class CharacterBaseStat
{
    public string baseId;
    public string name;
    public string key;
    public int baseAtk;
    public int baseDef;
    public int baseSpeed;
    public int baseHp;
    public string description;
    public string image;
    public GameCharacterRarity rarity;
    public GameCharacterRace race;
    public GameCharacterElement element;
}

public class Character : CharacterBaseStat
{
    public string id;
    public int atk;
    public int def;
    public int speed;
    public int hp;

    public int exp;
    public int level;

    public int position;

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

public class FormationCharacter
{
    public string characterId;
    public int position;
}
