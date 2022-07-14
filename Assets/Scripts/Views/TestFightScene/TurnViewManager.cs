using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnViewManager : MonoBehaviour
{
    public GameObject turnText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTurnText(int turn)
    {
        turnText.GetComponent<TextMeshProUGUI>().text = "Turn: " + turn;
    }
}
