using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollViewManager : MonoBehaviour
{
    public delegate void OnEventTriggered<T>(T data);
    public static event OnEventTriggered<int> OnCharacterSelected;
    public delegate void OnInstantiateEvent(GameObject obj, int index);
    public static event OnInstantiateEvent OnInstantiate;
    public GameObject charactersHolder;
    // Start is called before the first frame update
    public void Init()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RenderAvatar(GameObject avatarPrefab, int index, string id)
    {
        Debug.Log("RenderAvatar: " + index);
        avatarPrefab.GetComponent<AvatarController>().Init(index, id);
        GameObject obj = Instantiate(avatarPrefab, new Vector3(0, 0, 0), Quaternion.identity, charactersHolder.transform);
        OnInstantiate?.Invoke(obj, index);
    }
}
