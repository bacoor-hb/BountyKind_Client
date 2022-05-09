using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    protected string address;
    protected string name;
    protected int avatar;
    public User()
    {
        address = "NULL";
        name = "NULL";
        avatar = -1;
    }

    public User(string _address, string _name, int _avatarId)
    {
        address = _address;
        name = _name;
        avatar = _avatarId;
    }

    public string Address
    {
        get
        {
            return address;
        }
        set
        {
            address = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public int AvatarID
    {
        get
        {
            return avatar;
        }
        set
        {
            avatar = value;
        }
    }
}