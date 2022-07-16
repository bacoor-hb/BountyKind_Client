using UnityEngine;

public class CamController : MonoBehaviour
{
    public float speed = 3.5f;
    private float X;
    private float Y;
    [SerializeField]
    private GameObject board;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == gameObject.name)
                {
                    board.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed, -Input.GetAxis("Mouse X") * speed, 0));
                    X = board.transform.rotation.eulerAngles.x;
                    Y = board.transform.rotation.eulerAngles.y;
                    board.transform.rotation = Quaternion.Euler(0, Y, 0);
                }
            }

        }
    }
}