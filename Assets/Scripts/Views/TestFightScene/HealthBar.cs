using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform camera;

    private void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().transform;
    }
    void Start()
    {

    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}
