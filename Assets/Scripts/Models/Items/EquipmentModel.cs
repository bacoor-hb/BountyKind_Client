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