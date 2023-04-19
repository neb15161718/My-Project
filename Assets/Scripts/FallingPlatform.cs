using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool falling;
    public Transform player;

    void FixedUpdate()
    {
        if (falling)
        {
            transform.position = transform.position - new Vector3(0,1,0);
            player.parent = transform;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            falling = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            falling = false;
        }
    }
}
