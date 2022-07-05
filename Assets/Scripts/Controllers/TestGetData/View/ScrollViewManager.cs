using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollViewManager : MonoBehaviour
{
    public delegate void OnEventTriggered<T>(T data);
    public static OnEventTriggered<int> OnCharacterSelected;
    public delegate void OnInstantiateEvent(GameObject obj, int index);
    public static OnInstantiateEvent OnInstantiate;
    public GameObject charactersHolder;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RenderAvatar(GameObject avatarPrefab, int index, string id)
    {
        avatarPrefab.GetComponent<AvatarViewManager>().SetIndex(index);
        avatarPrefab.GetComponent<AvatarViewManager>().SetId(id);
        GameObject obj = Instantiate(avatarPrefab, new Vector3(0, 0, 0), Quaternion.identity, charactersHolder.transform);
        OnInstantiate?.Invoke(obj, index);
    }
}
