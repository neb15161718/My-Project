using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool falling;

    void FixedUpdate()
    {
        if (falling)
        {
            transform.position = transform.position - new Vector3(0,1,0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(2);
        falling = true;
        yield return new WaitForSeconds(5);
        falling = false;
        transform.position = new Vector3(transform.position.x, -1, transform.position.z);
    }
}
