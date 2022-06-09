using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitQueueController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Vector3 unitInitPosition;
    public float speed = 5f;
    private float startTime;
    [SerializeField]
    public string id;
    [SerializeField]
    private Vector3 endPositon = Vector3.zero;
    public delegate void OnEndQueue();
    public static event OnEndQueue onEndQueue;

    private bool isCurrentTarget;

    void Start()
    {
        endPositon = Vector3.zero;
        unitInitPosition = transform.position;
    }

    public void SetEndPosition(Vector3 _position)
    {
        endPositon = _position;
        startTime = Time.time;
    }

    public void SetIsCurrentTarget()
    {
        isCurrentTarget = true;
    }

    public void ResetPosition()
    {
        transform.position = unitInitPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (endPositon != Vector3.zero)
        {
            if (transform.position != endPositon)
            {
                float journeyLength = Vector3.Distance(transform.position, endPositon);
                float distCovered = (Time.time - startTime) * speed;
                float fractionOfJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(transform.position, endPositon, fractionOfJourney);
                if (isCurrentTarget)
                {
                    float d = Vector3.SqrMagnitude(endPositon - transform.position);
                    if (d < 130000f)
                    {
                        isCurrentTarget = false;
                        onEndQueue?.Invoke();
                    }
                }
            }
            else
            {
                Debug.Log("Reend");
                endPositon = Vector3.zero;
            }
        }
    }
}
