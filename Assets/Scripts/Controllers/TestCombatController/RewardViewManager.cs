using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardViewManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Image rewardImage;
    [SerializeField]
    private TextMeshProUGUI rewardAmt;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRewardImage(Sprite image)
    {
        rewardImage.sprite = image;
    }

    public void SetRewardAmt(int amt)
    {
        rewardAmt.text = amt.ToString();
    }
}
