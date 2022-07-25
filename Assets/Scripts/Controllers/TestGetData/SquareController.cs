using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SquareController : MonoBehaviour
{
    [SerializeField]
    private GameObject selectPlane;
    public int position;
    // Start is called before the first frame update
    void Start()
    {
        selectPlane.SetActive(false); ;
        FormationController.Instance.OnSelectedSquare += HandleOnSelectedSquare;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleOnSelectedSquare(int _position)
    {
        if (position == _position)
        {
            selectPlane.SetActive(true);
        }
        else
        {
            selectPlane.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        //Debug.Log("Detroy Square Controller");
        FormationController.Instance.OnSelectedSquare = null;
    }
}
