using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletManager: MonoBehaviour
{
    public delegate void OnEventTrigger<T>(T data);
    public OnEventTrigger<long> OnUpdateMoney;
    public WalletData walletData = null; 

    /// <summary>
    /// Initialize the wallet data and the User.
    /// </summary>
    /// <param name="_user"></param>
    public void Init(User _user)
    {
        walletData = new WalletData(_user);
        OnUpdateMoney = null;
    }

    /// <summary>
    /// Update the Money data.
    /// </summary>
    /// <param name="money"></param>
    public void OnYu2_Update(long money)
    {
        if(walletData == null || walletData.Owner == null)
        {
            Debug.LogError("[Waller Manager] Null ERROR, please Initialize the Manager first.");
        }
        if (OnUpdateMoney != null)
        {
            walletData.Yu2 = money;
            OnUpdateMoney?.Invoke(money);
        }
    }
}
