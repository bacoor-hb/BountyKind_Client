using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    [SerializeField]
    private Vector3 rootTransform;
    [SerializeField]
    private Transform rootParent;
    public GameObject panelParent;
    public GameObject board;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        rootParent = transform.parent;
    }

    public void SetBoard(GameObject _board)
    {
        board = _board;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.name.Contains("Postion"))
        {
            var endPostionGameObj = eventData.pointerCurrentRaycast.gameObject;
            transform.SetParent(endPostionGameObj.transform);
            rectTransform.anchoredPosition = endPostionGameObj.GetComponent<RectTransform>().localPosition;
        }
        else
        {
            transform.SetParent(rootParent);
            rectTransform.anchoredPosition = rootTransform;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rootTransform = GetComponent<RectTransform>().localPosition;
        transform.SetParent(board.transform);
    }
}
