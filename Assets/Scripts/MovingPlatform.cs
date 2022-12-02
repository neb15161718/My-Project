using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 endPosition;
    Vector3 target;

    void Start()
    {
        startPosition = GetComponentInChildren<PlatformStart>().transform.position;
        endPosition = GetComponentInChildren<PlatformEnd>().transform.position;
        target = endPosition;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 0.1f);
        if (transform.position == endPosition)
        {
            target = startPosition;
        }
        else if (transform.position == startPosition)
        {
            target = endPosition;
        }
    }
}
