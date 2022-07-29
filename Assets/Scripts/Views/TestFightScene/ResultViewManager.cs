using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultViewManager : MonoBehaviour
{
    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    private GameObject headerVictory;
    [SerializeField]
    private GameObject headerDefeated;
    public Button continueButton;
    [SerializeField]
    private GameObject rewardHolder;
    [SerializeField]
    private GameObject rewardPrefab;
    [SerializeField]
    private List<GameObject> instantiateRewardObjs;
    void Start()
    {
        instantiateRewardObjs = new List<GameObject>();
        resultPanel.SetActive(false);
        headerVictory.SetActive(false);
        headerVictory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator DisplayResultPanel(BattleData battleData)
    {
        yield return new WaitForSeconds(1);
        resultPanel.SetActive(true);
        if (battleData.status == 1)
        {
            if (instantiateRewardObjs != null && instantiateRewardObjs.Count > 0)
            {
                ResetRewards();
            }
            headerVictory.SetActive(true);
            if (battleData.rewards != null && battleData.rewards.Length > 0)
            {
                for (int i = 0; i < battleData.rewards.Length; i++)
                {
                    Reward_MSG currentReward = battleData.rewards[i];
                    GameObject rewardObj = rewardPrefab;
                    if (currentReward.type != RewardType.yu && currentReward.type != RewardType.energy)
                    {
                        Sprite rewardImage = Resources.Load<Sprite>("Rewards/" + currentReward.key);
                        rewardObj.GetComponent<RewardViewManager>().SetRewardImage(rewardImage);
                    }
                    else
                    {
                        Sprite rewardImage = Resources.Load<Sprite>("Rewards/" + currentReward.type.ToString());
                        rewardObj.GetComponent<RewardViewManager>().SetRewardImage(rewardImage);
                    }
                    rewardObj.GetComponent<RewardViewManager>().SetRewardAmt(currentReward.amount);
                    RenderReward(rewardPrefab);
                }
            }
        }
        else
        {
            headerDefeated.SetActive(true);
        }
    }

    public void HideResultPanel()
    {
        resultPanel.SetActive(false);
        headerVictory.SetActive(false);
        headerDefeated.SetActive(false);
    }

    public void ResetRewards()
    {
        instantiateRewardObjs.ForEach(obj =>
        {
            Destroy(obj);
        });
        instantiateRewardObjs = new List<GameObject>();
    }

    public void RenderReward(GameObject rewardPrefab)
    {
        instantiateRewardObjs.Add((Instantiate(rewardPrefab, rewardHolder.transform)));
    }
}
