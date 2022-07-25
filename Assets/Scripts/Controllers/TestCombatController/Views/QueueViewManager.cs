using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QueueViewManager : MonoBehaviour
{
    public delegate void OnEventTriggered<T>(T data);
    public OnEventTriggered<List<GameObject>> OnRenderQueueCompleted;
    public GameObject endObj;
    [SerializeField]
    private GameObject unitInQueuePrefab;
    [SerializeField]
    private GameObject scrollviewContent;
    [SerializeField]
    private GameObject scrollview;
    [SerializeField]
    private List<GameObject> sampleUnitsInQueue;
    [SerializeField]
    private GameObject currentUnit;
    // Start is called before the first frame update
    void Start()
    {
        LocalFightController.Instance.onRenderQueue += RenderQueue;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RenderQueue(List<UnitQueue> arr)
    {
        List<GameObject> unitsInQueue = new();
        for (var i = 0; i < arr.Count; i++)
        {
            Vector3 rootPos = sampleUnitsInQueue[0].transform.position;
            Vector3 pos = rootPos + new Vector3(i * 150, 0, 0);
            GameObject unit = Instantiate(unitInQueuePrefab, pos, Quaternion.identity, scrollviewContent.transform);
            unit.GetComponent<UnitQueueController>().id = arr[i].id;
            unit.GetComponent<UnitQueueController>().turn = arr[i].turn;
            Sprite avatarSprite = Resources.Load<Sprite>("Avatar/" + arr[i].baseKey);
            unit.transform.Find("Background").transform.Find("Avatar").GetComponent<Image>().sprite = avatarSprite;
            unitsInQueue.Add(unit);
        }
        OnRenderQueueCompleted?.Invoke(unitsInQueue);
    }

    public void SetCurrentUnit(List<GameObject> unitsOutQueue)
    {
        currentUnit.transform.Find("Avatar").GetComponent<Image>().sprite = unitsOutQueue[unitsOutQueue.Count - 1].transform.Find("Background").transform.Find("Avatar").GetComponent<Image>().sprite;
    }

    public void ResetCurrentUnit()
    {
        currentUnit.transform.Find("Avatar").GetComponent<Image>().sprite = null;
    }

    public void ScrollToLeft()
    {
        Vector3 oldPos = scrollviewContent.GetComponent<RectTransform>().localPosition;
        scrollviewContent.GetComponent<RectTransform>().localPosition = oldPos + new Vector3(-120, 0, 0);
    }
}
