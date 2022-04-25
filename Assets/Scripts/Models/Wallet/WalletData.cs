using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletData  
{
    private string _idUser;
    private string _addressWallet;
    private long _money;

    public WalletData (string idUser, string addressWallet, long money)
    {
        IDUser = idUser;
        AddressWallet = addressWallet;
        Money = money;
        
    }
    public WalletData()
    { 

    }
    public string IDUser
    {
        get
        {
            return _idUser;
        }
        set
        {
            _idUser = value;
        }
    }
   
    public string AddressWallet
    {
        get
        {
            return _addressWallet;
        }
        set
        {
            _addressWallet = value;
        }
    }

    public long Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
        }
    }


}
