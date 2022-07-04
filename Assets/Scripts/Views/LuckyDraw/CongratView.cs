using System.Collections;
using System.Collections.Generic;
using UI.ThreeDimensional;
using UnityEngine;
using UnityEngine.UI;

public class CongratView : MonoBehaviour
{
    private UIObject3D ItemPrefab;
    [SerializeField]
    private Button CloseBtn;

    public void OpenPopup(GameObject _itemPrefab)
    {
        CloseBtn.onClick.RemoveAllListeners();
        CloseBtn.onClick.AddListener(ClosePopup);

        if(_itemPrefab != null)
        {
            ItemPrefab.ObjectPrefab = _itemPrefab.transform;
            gameObject.SetActive(true);
        }        
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
