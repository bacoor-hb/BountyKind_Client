using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTextView : MonoBehaviour
{
    // Start is called before the first frame update
    public Text healthTextField;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText(string text)
    {
        healthTextField.text = text;
    }
}
