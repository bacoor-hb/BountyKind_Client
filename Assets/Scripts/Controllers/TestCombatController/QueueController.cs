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

    public void RenderQueue(List<UnitQueue> arr)
    {
        List<GameObject> unitsInQueue = new List<GameObject>();
        for (var i = 0; i < arr.Count; i++)
        {
            float xValue = sampleUnitsInQueue[0].transform.position.x + (85 * i);
            float yValue = sampleUnitsInQueue[0].transform.position.y;
            float zValue = sampleUnitsInQueue[0].transform.position.z;
            Vector3 position = new Vector3(xValue, yValue, zValue);
            GameObject unit = Instantiate(unitInQueue, position, Quaternion.identity, transform);
            unit.GetComponent<UnitQueueController>().id = arr[i].id;
            unit.GetComponent<UnitQueueController>().turn = arr[i].turn;
            Sprite avatarSprite = Resources.Load<Sprite>("Avatar/" + arr[i].id);
            unit.transform.Find("Background").transform.Find("Avatar").GetComponent<Image>().sprite = avatarSprite;
            unitsInQueue.Add(unit);
        }
        LocalTestFightController.SetUnitQueueList(unitsInQueue);
    }
}
