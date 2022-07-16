using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AvatarController : MonoBehaviour
{
    [SerializeField]
    private UserCharacter userCharacter;
    [SerializeField]
    private AvatarViewManager avatarViewManager;
    public void Init(int index, string id)
    {
        GenerateSprite();
        avatarViewManager.Init(index, id);
    }

    void GenerateSprite()
    {
        avatarViewManager.GenerateSprite(userCharacter);
    }

    public void SetUserCharacter(UserCharacter _userCharacter)
    {
        userCharacter = _userCharacter;
    }

    public UserCharacter GetUserCharacter()
    {
        return userCharacter;
    }

}
