using System;
using System.Collections.Generic;
using UnityEngine;

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

#region Character formation
[Serializable]
public class CharacterWithPosition
{
    public string characterId;
    public int position;
    public CharacterWithPosition(string _characterId, int _position)
    {
        characterId = _characterId;
        position = _position;
    }
}

[Serializable]
public class FormationCharacters
{
    public string _id;
    public string key;
    public string baseKey;
    public string status;
    public string[] itemList;
    public int atk;
    public int def;
    public int speed;
    public int hp;
    public int level;
    public string contractAddress;
    public string nftId;
    public int position;
}

[Serializable]
public class SetFormationResponse
{
    public string message;
}

[Serializable]
public class UserFormationResponse
{
    public string message;
    public FormationCharacters[] data;
}

[Serializable]
public class CharacterWithPositionList
{
    public List<CharacterWithPosition> data;
    public CharacterWithPositionList()
    {
        data = new List<CharacterWithPosition>();
    }
}
#endregion


#region User Character
[Serializable]
public class UserCharacters_API
{
    public UserCharacter[] data;
    public int limit;
    public int page;
    public int total;
    public int pages;
}

[Serializable]
public class UserCharacter
{
    public string _id;
    public string key;
    public string baseKey;
    public string name;
    public string status;
    public int level;
    public string race;
    public string element;
    public string type;
    public string contractAddress;
    public string nftId;
    public string ownerAddress;
    public string image;
}
#endregion