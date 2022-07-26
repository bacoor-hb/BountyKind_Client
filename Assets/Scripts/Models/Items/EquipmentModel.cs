using System;

public enum GameEquipmentType
{
    SWORD,
    GUN,
    SHIELD,
    CLAW,
    STAFF,
    DATA_UNIT,
    SOUL,
    SPIRIT,
};

[Serializable]
public class ItemData_MSG
{
    public string _id;
    public string key;
    public string name;
    public string description;
    public float atk;
    public float def;
    public float speed;
    public float hp;
    public string type;
    public string image;
    public bool active;
    public string contractToBuy;
    public string contractToMint;
    public string typeId;
}

[Serializable]
public class UserItemsResponse
{
    public UserItems_API data;
    public string message;
}

[Serializable]
public class UserItems_API
{
    public UserItem[] data;
    public int limit;
    public int page;
    public int total;
    public int pages;
}

[Serializable]
public class UserItem
{
    public string _id;
    public string key;
    public string baseKey;
    public string name;
    public string image;
    public string status;
    public bool isSelling;
    public string rarity;
    public float atk;
    public float def;
    public float speed;
    public float hp;
    public string type;
    public string contractAddress;
    public string nftId;
    public string ownerAddress;
}