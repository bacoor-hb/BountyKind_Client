using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QueueController : MonoBehaviour
{
    [SerializeField]
    private GameObject unitInQueue;
    [SerializeField]
    private List<GameObject> sampleUnitsInQueue;
    // Start is called before the first frame update
    void Start()
    {
        LocalTestFightController.onRenderQueue += RenderQueue;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RenderQueue(int[] arr)
    {
        List<GameObject> unitsInQueue = new List<GameObject>();
        for (var i = 0; i < arr.Length; i++)
        {
            GameObject unit = Instantiate(unitInQueue, sampleUnitsInQueue[i].transform.position, Quaternion.identity, transform);
            unit.GetComponent<UnitQueueController>().id = "id" + arr[i];
            Sprite avatarSprite = Resources.Load<Sprite>("Avatar/id" + arr[i]);
            unit.transform.Find("Background").transform.Find("Avatar").GetComponent<Image>().sprite = avatarSprite;
            unitsInQueue.Add(unit);
        }
        LocalTestFightController.SetUnitQueueList(unitsInQueue);
    }
}
