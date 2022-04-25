using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletManager : MonoBehaviour
{
    public delegate void EventHaveProps<T>(T data);
    public EventHaveProps<long> OnChangeMoney;
    public EventHaveProps<long> OnChangeMoneyWeb3;
    public WalletData _walletData = new WalletData(); 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnChangeMoneyEvent(long money)
    {
        if (OnChangeMoney!=null)
        {
            _walletData.Money += money;
            OnChangeMoney?.Invoke(money);
        }
    }
    void OnChangeMoneyWeb3Event(long money)
    {
        if (OnChangeMoney != null)
        {
            OnChangeMoney?.Invoke(money);
        }
    }
    public void setWalletUer(string iduser, string addressWallet, long money)
    {
        _walletData.IDUser = iduser;
        _walletData.AddressWallet = addressWallet;
        _walletData.Money = money;
    }
}
