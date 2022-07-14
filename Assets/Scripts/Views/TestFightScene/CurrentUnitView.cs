using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentUnitView : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject unitImage;
    [SerializeField]
    private Text levelText;
    [SerializeField]
    private Text atkText;
    [SerializeField]
    private Text defText;
    [SerializeField]
    private Text spdText;
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCurretUnitView(FormationCharacters currentUnit)
    {
        gameObject.SetActive(true);
        Sprite avatarSprite = Resources.Load<Sprite>("Avatar/" + currentUnit.baseKey);
        unitImage.GetComponent<Image>().sprite = avatarSprite;
        levelText.text = "Level " + currentUnit.level.ToString();
        atkText.text = "Attack: " + currentUnit.atk.ToString();
        defText.text = "Defense: " + currentUnit.def.ToString();
        spdText.text = "Speed: " + currentUnit.speed.ToString();
    }
}
