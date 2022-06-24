using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckyDrawController : MonoBehaviour
{
    private Dictionary<string, GameObject> itemPrefabList;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_itemPrefabList"></param>
    public void SetItemList(List<GameObject> _itemPrefabList)
    {
        itemPrefabList = new Dictionary<string, GameObject>();
        for(int i = 0; i < _itemPrefabList.Count; i++)
        {
            itemPrefabList.Add(_itemPrefabList[i].name, _itemPrefabList[i]);
        }
        
    }

    public void SetPrice()
    {

    }
}
