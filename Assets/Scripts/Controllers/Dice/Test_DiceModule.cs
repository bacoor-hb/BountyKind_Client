using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_DiceModule : MonoBehaviour
{
    [SerializeField]
    private DicesController dicesController;

    [SerializeField]
    int[] DiceValue;
    // Start is called before the first frame update
    void Start()
    {
        dicesController.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            dicesController.RollDice(DiceValue);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            dicesController.ResetDicesPosition();
        }
    }
}
