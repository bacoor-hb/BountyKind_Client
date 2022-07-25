using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SquareController : MonoBehaviour
{
    public delegate void OnEventMouse(int position);
    public OnEventMouse OnMouseDownEvent;
    [SerializeField]
    private GameObject selectPlane;
    public int position;
    public Color rootColor;
    // Start is called before the first frame update
    void Start()
    {
        rootColor = gameObject.GetComponent<Renderer>().material.color;
        selectPlane.SetActive(false);
        FormationController.Instance.OnSelectedSquare += HandleOnSelectedSquare;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        Debug.Log("Asdasd");
        OnMouseDownEvent?.Invoke(position);
    }

    private void OnMouseUp()
    {
        Debug.Log("up");
    }

    public void HandleOnSelectedSquare(int _position)
    {
        if (position == _position)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", rootColor);
        }
    }

    private void OnDestroy()
    {
        //Debug.Log("Detroy Square Controller");
        FormationController.Instance.OnSelectedSquare = null;
    }
}
