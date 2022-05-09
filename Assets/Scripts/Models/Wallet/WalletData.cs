using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletData  
{
    private User owner;
    private long yu2;
    private int totalDice;

    public WalletData (User _user, long _yu2 = 0, int _totalDice = 0)
    {
        owner = _user;
        Yu2 = _yu2;
        totalDice = _totalDice;
    }
    public WalletData()
    { 

    }
    public User Owner
    {
        get
        {
            return owner;
        }
        private set
        {
           
        }
    }
    public long Yu2
    {
        get
        {
            return yu2;
        }
        set
        {
            yu2 = value;
        }
    }

    public int TotalDice
    {
        get
        {
            return totalDice;
        }
        set
        {
            totalDice = value;
        }
    }
}
