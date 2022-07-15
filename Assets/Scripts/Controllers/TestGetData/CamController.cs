using UnityEngine;

public class CamController : MonoBehaviour
{
    public float speed = 3.5f;
    private float X;
    private float Y;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed, -Input.GetAxis("Mouse X") * speed, 0));
            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, Y, 0);
        }
    }
}