using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightSceneUnitView : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject healthSlider;
    [SerializeField]
    private HealthTextView healthTextView;
    public UnitOutline unitOutline;

    private void Awake()
    {
        unitOutline.OutlineWidth = 0f;
        unitOutline.OutlineColor = Color.cyan;
        unitOutline.outlineMode = UnitOutline.Mode.OutlineAll;
        healthSlider.GetComponent<Slider>().value = 1f;
    }
    void Start()
    {
        unitOutline.OutlineWidth = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetOutlineWidth(float width)
    {
        unitOutline.OutlineWidth = width;
    }

    public void SetSliderValue(float value)
    {
        healthSlider.GetComponent<Slider>().value = value;
    }

    public void SetSliderColor(Color color)
    {
        healthSlider.GetComponent<Slider>().transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;
    }

    public void SetHealthText(string text)
    {
        healthTextView.SetText(text);
    }
}
