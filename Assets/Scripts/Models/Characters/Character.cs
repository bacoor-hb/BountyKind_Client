using System;

[Serializable]
public enum CharacterStatus 
{
    ACTIVE,
    ONSALE,
    ONRENT,
    DESTROYED,
};

[Serializable]
public class NFTItem
{
    public string addressContract;
    public string nftId;
}

[Serializable]
public class Character
{
    public string name;
    public int atk;
    public int def;
    public int speed;
    public int hp;
    public int baseAtk;
    public int baseSpeed;
    public int baseHp;
    public int exp;
    public int level;

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
