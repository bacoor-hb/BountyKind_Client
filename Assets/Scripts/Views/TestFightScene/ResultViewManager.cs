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
    void Start()
    {
        resultPanel.SetActive(false);
        headerVictory.SetActive(false);
        headerVictory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator DisplayResultPanel(int result)
    {
        yield return new WaitForSeconds(1);
        resultPanel.SetActive(true);
        if (result == 1)
        {
            headerVictory.SetActive(true);
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
        headerVictory.SetActive(false);
    }
}
