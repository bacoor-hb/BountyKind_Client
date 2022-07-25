using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AvatarViewManager : MonoBehaviour
{
    [SerializeField]
    private GameObject border;
    [SerializeField]
    private GameObject blockBG;
    public int index;
    public string id;
    // Start is called before the first frame update
    public void Init(int _index, string _id)
    {
        //Debug.Log("InitAvatar");
        blockBG.SetActive(false);
        border.SetActive(false);
        index = _index;
        id = _id;
        FormationController.Instance.OnFormationCharatecterReceived = null;
        FormationController.Instance.OnFormationCharatecterReceived += HandleOnFormationCharatecterReceived;
    }

    public void SetIndex(int _index)
    {
        index = _index;
    }
    public void SetId(string _id)
    {
        id = _id;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateSprite(UserCharacter userCharacter)
    {
        Sprite avatarSprite = Resources.Load<Sprite>("FormationAvatar/" + userCharacter.baseKey);
        transform.Find("Background").GetComponent<Image>().sprite = avatarSprite;
    }

    public void HandleOnFormationCharatecterReceived(string _id)
    {
        if (id == _id)
        {
            blockBG.SetActive(true);
        }
    }

    public void Toggle(bool state)
    {
        border.SetActive(state);
    }

    public void ActiveBlockBG()
    {
        blockBG.SetActive(true);
    }

    private void OnDestroy()
    {
        //Debug.Log("DestroyAvatarView");
    }
}
