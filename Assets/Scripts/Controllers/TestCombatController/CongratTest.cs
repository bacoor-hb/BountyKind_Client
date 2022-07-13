using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongratTest : MonoBehaviour
{
    [SerializeField]
    private GameObject winningObject;
    [SerializeField]
    private CongratView view;

    // Start is called before the first frame update
    void Start()
    {
        view.Init(winningObject);
    }
}
